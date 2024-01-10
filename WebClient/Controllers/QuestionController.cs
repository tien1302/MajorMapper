using BAL.DTOs.Accounts;
using BAL.DTOs.Majors;
using BAL.DTOs.PersonalityTypes;
using BAL.DTOs.Questions;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class QuestionController : Controller
    {
        private readonly HttpClient client;
        private string baseApiUrl = "";
        private string personApiUrl = "";
        public QuestionController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = "http://localhost:1189/api/Question";
            personApiUrl = "http://localhost:1189/api/PersonalityType";
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
                List<GetQuestion> list = JsonSerializer.Deserialize<List<GetQuestion>>(strData, options);
                return View(list);
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Tài khoản bạn không có quyền sử dụng chức năng này.";
                return Redirect("~/Home/Index");
            }
            
        }
        public async Task<IActionResult> Processing()
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
                HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/GetProcessing");
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<GetQuestion> list = JsonSerializer.Deserialize<List<GetQuestion>>(strData, options);
                return View(list);
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
                return Redirect("~/Home/Index");
            }
            
        }
        public async Task<ActionResult> Create(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Consultant")
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Consultant.";
                return Redirect("~/Home/Index");
            }
            try
            {
                //Token
                var accessToken = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //Personality Type
                HttpResponseMessage response = await client.GetAsync(personApiUrl);
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<GetPersonalityType> list = JsonSerializer.Deserialize<List<GetPersonalityType>>(strData, options);
                if (list != null)
                    ViewBag.ListPerson = list.Select(
                        i => new SelectListItem
                        {
                            Text = i.Name,
                            Value = i.Id.ToString()
                        });
                //Question
                HttpResponseMessage responseq = await client.GetAsync($"{baseApiUrl}/{id}");
                if (responseq.IsSuccessStatusCode)
                {
                    var strDataq = await responseq.Content.ReadAsStringAsync();
                    var optionsq = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    CreateQuestion question = JsonSerializer.Deserialize<CreateQuestion>(strDataq, optionsq);
                 
                    return View(question);
                }
                return View();
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Consultant.";
                return Redirect("~/Home/Index");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateQuestion p)
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
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Thêm câu hỏi thành công.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["AlertMessageError"] = "Thêm câu hỏi thất bại.";
                    }
                }
                else
                    TempData["AlertMessageError"] = "Thêm câu hỏi thất bại.";
                return View("Create");
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Consultant.";
                return Redirect("~/Home/Index");
            }
        }

        public async Task<IActionResult> Update(int id, string status)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
                return Redirect("~/Home/Index");
            }
            try
            {
                UpdateQuestion question = new UpdateQuestion();
                question.Status = status;
                if (ModelState.IsValid)
                {
                    //Token
                    var accessToken = HttpContext.Session.GetString("JWToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    //Update
                    string strData = JsonSerializer.Serialize(question);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/{id}", contentData);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Cập nhật câu hỏi thành công.";
                    }
                    else
                    {
                        TempData["AlertMessageError"] = "Cập nhật câu hỏi thất bại.";
                    }
                }
                else
                    TempData["AlertMessageError"] = "Cập nhật câu hỏi thất bại.";
                return RedirectToAction(nameof(Processing));
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
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
                    TempData["AlertMessage"] = "Xóa câu hỏi thành công.";
                }
                else
                {
                    TempData["AlertMessageError"] = "Xóa câu hỏi thất bại.";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
                return Redirect("~/Home/Index");
            }
        }
    }
}
