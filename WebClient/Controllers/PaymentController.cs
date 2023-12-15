using BAL.DAOs.Interfaces;
using BAL.DTOs.Bookings;
using BAL.DTOs.Payments;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class PaymentController : Controller
    {
        private readonly HttpClient client;
        private string baseApiUrl = "";

        public PaymentController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = "http://localhost:1189/api/Payment";
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreatePaymentUrlAsync(GetBooking p)
        {
            CreatePayment model = new CreatePayment()
            {
                PlayerId = p.UserId,
                BookingId = p.Id,
                Amount = 50000,
                Description = "Thanh toán booking",
                OrderId = "",
                TransactionId = "",
                SecureHash = "",
            };

            string strData = JsonSerializer.Serialize(model);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{baseApiUrl}/CreatePaymentUrl", contentData);
            var url = await response.Content.ReadAsStringAsync();

            string link = url.Replace("\"", "");
            TempData["CreatePayment"] = strData;
            return Redirect(link);
        }

        public async Task<IActionResult> PaymentCallbackAsync()
        {
            IQueryCollection collections = Request.Query;
            // Get serialized data from TempData
            string serializedData = TempData["CreatePayment"] as string;

            // Deserialize serialized data back to CreatePayment object
            CreatePayment model = JsonSerializer.Deserialize<CreatePayment>(serializedData);
            model.SecureHash = collections["vnp_SecureHash"];
            model.OrderId = collections["vnp_TxnRef"];
            model.TransactionId = collections["vnp_TransactionNo"];

            string strData = JsonSerializer.Serialize(model);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{baseApiUrl}/PaymentCallback", contentData);
            return Json(response);
        }
    }
}
