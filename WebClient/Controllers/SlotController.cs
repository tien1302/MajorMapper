using BAL.DTOs.Slots;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class SlotController : Controller
    {
        private readonly HttpClient client;
        private string baseApiUrl = "";

        public SlotController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = baseApiUrl = "http://localhost:1189/api/Slot";
        }

        public async Task<ActionResult> Index()
        {
            var id = HttpContext.Session.GetInt32("AccountId");
            HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetSlot> list = JsonSerializer.Deserialize <List<GetSlot>>(strData, options);
            ViewData["Slots"] = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CreateSlot p)
        {
            string strData = JsonSerializer.Serialize(p);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(baseApiUrl, contentData);
            var message = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = message.Replace("\"", "");
            var id = HttpContext.Session.GetInt32("AccountId");
            HttpResponseMessage responseSlot = await client.GetAsync($"{baseApiUrl}/{id}");
            string strDataSlot = await responseSlot.Content.ReadAsStringAsync();

            var optionsSlot = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetSlot> list = JsonSerializer.Deserialize<List<GetSlot>>(strDataSlot, optionsSlot);
            ViewData["Slots"] = list;
            return View();
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

		public IActionResult Call()
		{
			return View();
		}

	}
}
