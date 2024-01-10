using Azure.Core;
using BAL.DTOs.Accounts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClient.Models;
using DAL.Models;
using BAL.DTOs.Authentications;
using Microsoft.AspNetCore.Http;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client;
        private readonly ILogger<HomeController> _logger;
        private string baseApiUrl = "";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = "http://localhost:1189/api/Account";
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login (AuthenticationAccount account)
        {
            //Login
            string strData = JsonSerializer.Serialize(account);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{baseApiUrl}/Login", contentData);
            var token = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                //Set Session
                GetAccount tokenResponse = JsonSerializer.Deserialize<GetAccount>(token, options);
                HttpContext.Session.SetInt32("AccountId", tokenResponse.Id);
                HttpContext.Session.SetString("Name", tokenResponse.Name);
                HttpContext.Session.SetString("JWToken", tokenResponse.AccessToken);
                HttpContext.Session.SetString("Role", tokenResponse.RoleName);
          
                if (HttpContext.Session.GetString("Role") == "Admin")
                {
                    return Redirect("~/Account/Index");
                }
                if (HttpContext.Session.GetString("Role") == "Consultant")
                {
                    return Redirect("~/Slot/Index");
                }
                ViewBag.Message = "Tài khoản bạn không có quyền sử dụng chức năng này.";
                return Redirect("~/Home/Index");
            }
            ViewBag.Message = token.Replace("\"", "");
            return View("Index");
        }

        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            //Google
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var emailAddress = result.Principal.Identities.FirstOrDefault()
                .Claims.Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
                .Select(x => x.Value)
                .FirstOrDefault();
            //Account
            AuthenticationAccountGoogle accountGoogle = new AuthenticationAccountGoogle();
            accountGoogle.Email = emailAddress;
            string strData = JsonSerializer.Serialize(accountGoogle);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://localhost:1189/api/Account/LoginGoogle", contentData);
            //Token
            var token = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                //Set Session
                GetAccount tokenResponse = JsonSerializer.Deserialize<GetAccount>(token,options);
                HttpContext.Session.SetInt32("AccountId", tokenResponse.Id);
                HttpContext.Session.SetString("Name", tokenResponse.Name);
                HttpContext.Session.SetString("JWToken", tokenResponse.AccessToken);
                HttpContext.Session.SetString("Role", tokenResponse.RoleName);

                if (HttpContext.Session.GetString("Role") == "Admin")
                {
                    return Redirect("~/Account/Index");
                }
                if (HttpContext.Session.GetString("Role") == "Consultant")
                {
                    return Redirect("~/Slot/Index");
                }
                ViewBag.Message = "Tài khoản bạn không có quyền sử dụng chức năng này.";
                return Redirect("~/Home/Index");
            }
            ViewBag.Message = token.Replace("\"", "");
            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            return Redirect("~/Home/Index");
        }
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}