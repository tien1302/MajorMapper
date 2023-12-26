using AgoraIO.Media;
using BAL.DTOs.Bookings;
using BAL.DTOs.Slots;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using NuGet.Common;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebClient.Controllers
{
    public class SlotController : Controller
    {
        private readonly HttpClient client;
        private string baseApiUrl = "";
        private string bookingApiUrl = "";

        public SlotController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = "http://localhost:1189/api/Slot";
            bookingApiUrl = "http://localhost:1189/api/Booking";
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
            HttpResponseMessage response = await client.PostAsync($"{baseApiUrl}?allDay={p.AllDay}&auto={p.Auto}", contentData);
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

        public async Task<IActionResult> Lobby()
        {
            string bookingId = Request.Query["bookingId"];
            HttpResponseMessage response = await client.GetAsync($"{bookingApiUrl}/{bookingId}");
            var strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            GetBooking booking = JsonSerializer.Deserialize<GetBooking>(strData, options);
            ViewData["Booking"] = booking;
            return View();
        }
        
        public async Task<ActionResult> GetToken(string channel)
        {
            string appId = "32f662b1d5cf4a50bbf47cd0ba9bfcd5";
            string appCertificate = "b1f5ac0e01f04a58a3fb5f6c43b903c4";
            uint uid = (uint)new Random().Next(1, 230);
            uint expirationTimeInSeconds = 3600;
            uint currentTimeStamp = (uint)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            uint privilegeExpiredTs = currentTimeStamp + expirationTimeInSeconds;
            RtcTokenBuilder.Role role = RtcTokenBuilder.Role.RolePublisher;

            string token = RtcTokenBuilder.buildTokenWithUID(appId, appCertificate, channel, uid, role, privilegeExpiredTs);


            return Json(new { token = token, uid = uid });
        }

        public IActionResult Room()
        {
            return View();
        }

    }
}
