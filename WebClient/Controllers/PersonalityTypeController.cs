using BAL.DTOs.Methods;
using BAL.DTOs.PersonalityTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class PersonalityTypeController : Controller
    {
        private readonly HttpClient client;
        private string baseApiUrl = "";
        private string methodApiUrl = "";

        public PersonalityTypeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = "http://localhost:1189/api/PersonalityType";
            methodApiUrl = "http://localhost:1189/api/Method";
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
				List<GetPersonalityType> list = JsonSerializer.Deserialize<List<GetPersonalityType>>(strData, options);
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
            //Method
			HttpResponseMessage response = await client.GetAsync(methodApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetMethod> list = JsonSerializer.Deserialize<List<GetMethod>>(strData, options);
            if (list != null)

                ViewBag.ListMethod = list.Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePersonalityType p)
        {
			try
			{
				if (ModelState.IsValid)
				{
					//Token
					var accessToken = HttpContext.Session.GetString("JWToken");
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
					//person
					string strData = JsonSerializer.Serialize(p);
					var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
					HttpResponseMessage response = await client.PostAsync(baseApiUrl, contentData);
					if (response.IsSuccessStatusCode)
					{
						TempData["AlertMessage"] = "Thêm tính cách thành công.";
						return RedirectToAction(nameof(Index));
					}
					else
					{
						TempData["AlertMessageError"] = "Thêm tính cách thất bại.";
					}
				}
				else
					TempData["AlertMessageError"] = "Thêm tính cách thất bại.";
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
                //Method
                HttpResponseMessage responsem = await client.GetAsync(methodApiUrl);
                string strDatam = await responsem.Content.ReadAsStringAsync();

                var optionsm = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<GetMethod> list = JsonSerializer.Deserialize<List<GetMethod>>(strDatam, optionsm);
                if (list != null)

                    ViewBag.ListMethod = list.Select(
                        i => new SelectListItem
                        {
                            Text = i.Name,
                            Value = i.Id.ToString()
                        });
                //Token
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
				//Get
				HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/{id}");
				var strData = await response.Content.ReadAsStringAsync();
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};
				UpdatePersonalityType personalityType = JsonSerializer.Deserialize<UpdatePersonalityType>(strData, options);
				return View(personalityType);
			}
			catch (Exception)
			{
				TempData["AlertMessageError"] = "Bạn phải đăng nhập bằng tài khoản Admin.";
				return Redirect("~/Home/Index");
			}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, UpdatePersonalityType p)
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
						TempData["AlertMessage"] = "Cập nhật tính cách thành công.";
						return RedirectToAction(nameof(Index));
					}
					else
					{
						TempData["AlertMessageError"] = "Cập nhật tính cách thất bại.";
					}
				}
				else
					TempData["AlertMessageError"] = "Cập nhật tính cách thất bại.";
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
