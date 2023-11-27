using BAL.DTOs.Accounts;
using BAL.DTOs.PersonalityTypes;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class PersonalityTypeController : Controller
    {
        private readonly HttpClient client;
        private string baseApiUrl = "";

        public PersonalityTypeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = baseApiUrl = "http://localhost:1189/api/PersonalityType";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(baseApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetPersonalityType> list = JsonSerializer.Deserialize<List<GetPersonalityType>>(strData, options);
            return View(list);
        }
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePersonalityType p)
        {
            string strData = JsonSerializer.Serialize(p);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(baseApiUrl, contentData);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Create");
        }
        public async Task<IActionResult> Update(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/{id}");
            var strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            UpdatePersonalityType personalityType = JsonSerializer.Deserialize<UpdatePersonalityType>(strData, options);
            return View(personalityType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, UpdatePersonalityType p)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(p);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/{id}", contentData);
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
            HttpResponseMessage response = await client.DeleteAsync($"{baseApiUrl}/{id}");
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
