using BAL.DTOs.Accounts;
using BAL.DTOs.Feedbacks;
using BAL.DTOs.TestResults;
using DAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient client;
        private string baseApiUrl = "";
        private string testApiUrl = "";
        private string feedbackApiUrl = "";
        public AccountController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = "http://localhost:1189/api/Account";
            feedbackApiUrl = "http://localhost:1189/api/Feedback";

        }
        public async Task<IActionResult> Index()
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
                //Account
                HttpResponseMessage response = await client.GetAsync(baseApiUrl);
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<GetAccount> list = JsonSerializer.Deserialize<List<GetAccount>>(strData, options);
                return View(list);
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
                return Redirect("~/Home/Index");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            //Token
            var accessToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //Session
            int currentId = (int)HttpContext.Session.GetInt32("AccountId");
            string role = HttpContext.Session.GetString("Role");
            if (currentId != id && role != "Admin")
            {
                return RedirectToAction("Details", new { id = currentId });
            }
            //Account
            HttpResponseMessage response = await client.GetAsync(baseApiUrl);
            var strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetAccount> list = JsonSerializer.Deserialize<List<GetAccount>>(strData, options);
            GetAccount account = list.Where(p => p.Id == id).FirstOrDefault();
            //Feedback
            HttpResponseMessage feedbackResponse = await client.GetAsync($"{feedbackApiUrl}/{id}");
            var feedbackData = await feedbackResponse.Content.ReadAsStringAsync();

            var optionF = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetFeedback> listFeedback = JsonSerializer.Deserialize<List<GetFeedback>>(feedbackData, optionF);
            ViewBag.Feedbacks = listFeedback;
            //TestResult
            HttpResponseMessage testResultResponse = await client.GetAsync($"{baseApiUrl}/GetTestResult/{id}");
            var testResultData = await testResultResponse.Content.ReadAsStringAsync();

            var optionT = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetTestResult> listTestResult = JsonSerializer.Deserialize<List<GetTestResult>>(testResultData, optionT);
            ViewBag.TestResults = listTestResult;
            return View(account);
        }

        public async Task<IActionResult> DetailsPlayer(int id)
        {
            //Token
            var accessToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //Account
            HttpResponseMessage response = await client.GetAsync(baseApiUrl);
            var strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetAccount> list = JsonSerializer.Deserialize<List<GetAccount>>(strData, options);
            GetAccount account = list.Where(p => p.Id == id).FirstOrDefault();
            //TestResult
            HttpResponseMessage testResultResponse = await client.GetAsync($"{baseApiUrl}/GetTestResult/{id}");
            var testResultData = await testResultResponse.Content.ReadAsStringAsync();
            var optionT = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetTestResult> listTestResult = JsonSerializer.Deserialize<List<GetTestResult>>(testResultData, optionT);
            ViewBag.TestResults = listTestResult;
            return View(account);
        }


        public async Task<ActionResult> Create()
        {
            if (HttpContext.Session.GetString("Role") !="Admin")
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
                return Redirect("~/Home/Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAccount p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Token
                    var accessToken = HttpContext.Session.GetString("JWToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    // Create Account
                    string strData = JsonSerializer.Serialize(p);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(baseApiUrl, contentData);
                    var token = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Thêm tài khoản thành công.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = token.Replace("\"", "");
                    }
                }
                else
                    TempData["AlertMessageError"] = "Thêm tài khoản thất bại.";
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
            try
            {
                //Token
                var accessToken = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //Session
                int currentId = (int)HttpContext.Session.GetInt32("AccountId");
                string role = HttpContext.Session.GetString("Role");
                if (currentId != id && role != "Admin")
                {
                    return RedirectToAction("Update", new { id = currentId });
                }
                //Update Account
                HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/{id}");
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                UpdateAccount account = JsonSerializer.Deserialize<UpdateAccount>(strData, options);
                return View(account);
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Tài khoản bạn không có quyền sử dụng chức năng này.";
                return Redirect("~/Home/Index");
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id,UpdateAccount p)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //Token
                    var accessToken = HttpContext.Session.GetString("JWToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    //Update Account
                    string strData = JsonSerializer.Serialize(p);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/{id}", contentData);
                    var token2 = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        if(HttpContext.Session.GetString("Role") != "Admin")
                        {
                            //Token
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                            //Update
                            HttpResponseMessage response1 = await client.GetAsync($"{baseApiUrl}/{id}");
                            var token = await response1.Content.ReadAsStringAsync();
                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };
                            GetAccount tokenResponse = JsonSerializer.Deserialize<GetAccount>(token, options);
                            HttpContext.Session.SetString("Name", tokenResponse.Name);
                            TempData["AlertMessage"] = "Cập nhật tài khoản thành công.";
                            return RedirectToAction(nameof(Details), new { id = tokenResponse.Id });
                        }
                        TempData["AlertMessage"] = "Cập nhật tài khoản thành công.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = token2.Replace("\"", "");
                    }
                }
                else
                    TempData["AlertMessageError"] = "Cập nhật tài khoản thất bại.";
                return View("Update");
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Tài khoản bạn không có quyền sử dụng chức năng này.";
                return Redirect("~/Home/Index");
            }
          
        }

        public async Task<IActionResult> Delete(int? id)
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
                //Delete
                HttpResponseMessage response = await client.DeleteAsync($"{baseApiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "Xóa tài khoản thành công.";
                }
                else
                {
                    TempData["AlertMessageError"] = "Xóa tài khoản thất bại.";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
                return Redirect("~/Home/Index");
            }
            
        }
        public async Task<ActionResult> ResetPassword()
        {
            if (HttpContext.Session.GetString("Role") != "Consultant")
            {
                TempData["AlertMessageError"] = "Tài khoản bạn không có quyền sử dụng chức năng này.";
                return Redirect("~/Home/Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassword p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Token
                    var accessToken = HttpContext.Session.GetString("JWToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    //Reset Password
                    string strData = JsonSerializer.Serialize(p);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/ResetPassword", contentData);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Cập nhật mật khẩu thành công.";
                    }
                    else
                    {
                        TempData["AlertMessageError"] = "Cập nhật mật khẩu thất bại.";
                    }
                }
                
                return View("ResetPassword");
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Consultant.";
                return Redirect("~/Home/Index");
            }
           
        }

    }
}
