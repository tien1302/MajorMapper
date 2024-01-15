using BAL.DTOs.Majors;
using BAL.DTOs.Methods;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class MethodController : Controller
    {
        private readonly HttpClient client;
        private string baseApiUrl = "";

        public MethodController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = "http://localhost:1189/api/Method";
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                //Token
                var accessToken = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //Get
                HttpResponseMessage response = await client.GetAsync(baseApiUrl);
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<GetMethod> list = JsonSerializer.Deserialize<List<GetMethod>>(strData, options);
                return View(list);
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Tài khoản bạn không có quyền sử dụng chức năng này.";
                return Redirect("~/Home/Index");
            }
            
        }
        public async Task<ActionResult> Create()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
                return Redirect("~/Home/Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMethod p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Token
                    var accessToken = HttpContext.Session.GetString("JWToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    //Create
                    string strData = JsonSerializer.Serialize(p);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(baseApiUrl, contentData);
                    var token = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Thêm phương pháp thành công.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = token.Replace("\"", "");
                    }
                }
                else
                    TempData["AlertMessageError"] = "Thêm phương pháp thất bại.";
                return View("Create");
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
                return Redirect("~/Home/Index");
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
                return Redirect("~/Home/Index");
            }
            try
            {
                //Token
                var accessToken = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //Get
                HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/{id}");
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                UpdateMethod method = JsonSerializer.Deserialize<UpdateMethod>(strData, options);
                return View(method);
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
                return Redirect("~/Home/Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update([FromRoute] int id, UpdateMethod p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Token
                    var accessToken = HttpContext.Session.GetString("JWToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    //Update
                    string strData = JsonSerializer.Serialize(p);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/{id}", contentData);
                    var token = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Cập nhật phương pháp thành công.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = token.Replace("\"", "");
                    }
                }
                
                return View("Update");
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
                return Redirect("~/Home/Index");
            }
        }
    }
}
