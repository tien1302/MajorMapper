using BAL.DTOs.Accounts;
using BAL.DTOs.Majors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class MajorController : Controller
    {
        private readonly HttpClient client;
        private string baseApiUrl = "";
        public MajorController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = "http://localhost:1189/api/Major";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(baseApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetMajor> list = JsonSerializer.Deserialize<List<GetMajor>>(strData, options);
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/{id}");
            var strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            GetMajor major = JsonSerializer.Deserialize<GetMajor>(strData, options);
            return View(major);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMajor p)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(p);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(baseApiUrl, contentData);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Insert successfully!";
                }
                else
                {
                    ViewBag.Message = "Error while calling WebAPI!";
                }
            }
            ViewBag.Message = "Error!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/{id}");
            var strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            UpdateMajor major = JsonSerializer.Deserialize<UpdateMajor>(strData, options);
            return View(major);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update([FromRoute] int id, UpdateMajor p)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(p);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsJsonAsync($"{baseApiUrl}/{id}", contentData);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Insert successfully!";
                }
                else
                {
                    ViewBag.Message = "Error while calling WebAPI!";
                }
            }
            else
                ViewBag.Message = "Error!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            HttpResponseMessage response = await client.DeleteAsync(baseApiUrl + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Delete successfully!";
            }
            else
            {
                TempData["Message"] = "Error while calling WebAPI!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
