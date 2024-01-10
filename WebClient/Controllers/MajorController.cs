using BAL.DTOs.Majors;
using BAL.DTOs.PersonalityTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class MajorController : Controller
    {
        private readonly HttpClient client;
        private string baseApiUrl = "";
        private string personApiUrl = "";
        public MajorController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = "http://localhost:1189/api/Major";
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
                List<GetMajor> list = JsonSerializer.Deserialize<List<GetMajor>>(strData, options);
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
            //Token
            var accessToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //Personality
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMajor p)
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
                        TempData["AlertMessage"] = "Thêm ngành nghề thành công.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["AlertMessageError"] = "Thêm ngành nghề thất bại.";
                    }
                }
                else
                    TempData["AlertMessageError"] = "Thêm ngành nghề thất bại.";
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
                //Personality
                HttpResponseMessage responsep = await client.GetAsync(personApiUrl);
                string strDatap = await responsep.Content.ReadAsStringAsync();
                var optionsp = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<GetPersonalityType> list = JsonSerializer.Deserialize<List<GetPersonalityType>>(strDatap, optionsp);
                if (list != null)

                    ViewBag.ListPerson = list.Select(
                        i => new SelectListItem
                        {
                            Text = i.Name,
                            Value = i.Id.ToString()
                        });
                //Get
                HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/{id}");
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                UpdateMajor major = JsonSerializer.Deserialize<UpdateMajor>(strData, options);
                return View(major);
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
                return Redirect("~/Home/Index");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update([FromRoute] int id, UpdateMajor p)
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
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Cập nhật ngành nghề thành công.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["AlertMessageError"] = "Cập nhật ngành nghề thất bại.";
                    }
                }
                else
                    TempData["AlertMessageError"] = "Cập nhật ngành nghề thất bại.";
                return View("Update");
            }
            catch (Exception)
            {
                TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
                return Redirect("~/Home/Index");
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                //Token
                var accessToken = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //Delete
                HttpResponseMessage response = await client.DeleteAsync($"{baseApiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "Xóa ngành nghề thành công.";
                }
                else
                {
                    TempData["AlertMessageError"] = "Xóa ngành nghề thất bại.";
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
