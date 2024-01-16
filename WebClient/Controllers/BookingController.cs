using BAL.DTOs.Accounts;
using BAL.DTOs.Bookings;
using BAL.DTOs.Slots;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class BookingController : Controller
    {
        private readonly HttpClient client;
        private string baseApiUrl = "";
        private string slotApiUrl = "";

        public BookingController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = "http://localhost:1189/api/Booking";
            slotApiUrl = "http://localhost:1189/api/Slot";
        }

        public async Task<ActionResult> Index()
        {
           
            HttpResponseMessage response = await client.GetAsync($"{slotApiUrl}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetSlot> list = JsonSerializer.Deserialize<List<GetSlot>>(strData, options);
            ViewData["Slots"] = list;
            return View();
        }

        public async Task<ActionResult> Create()
        {
            //Token
            var accessToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string slotId = Request.Query["slotId"];
            HttpResponseMessage response = await client.GetAsync($"{slotApiUrl}/GetById/{slotId}");
            var strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            GetSlot slot = JsonSerializer.Deserialize<GetSlot>(strData, options);
            ViewData["SlotBooking"] = slot;


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBooking p)
        {
            if (ModelState.IsValid)
            {
                //Token
                var accessToken = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                string strData = JsonSerializer.Serialize(p);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(baseApiUrl, contentData);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var options1 = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    GetBooking booking = JsonSerializer.Deserialize<GetBooking>(responseBody, options1);
                    // Deserialize the response body to get the ID
                    return RedirectToAction("CreatePaymentUrl", "Payment", booking);
                }
                else
                {
                    ViewBag.Message = "Error while calling WebAPI!";
                }
            }
            ViewBag.Message = "Error!";

            return Redirect("~/Payment/CreatePaymentUrl");
        }
    }
}
