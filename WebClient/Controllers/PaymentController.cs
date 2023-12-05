using BAL.DAOs.Interfaces;
using BAL.DTOs.Bookings;
using BAL.DTOs.Payments;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class PaymentController : Controller
    {
        private readonly HttpClient client;
        private IPaymentDAO _paymentDAO;
        //private string baseApiUrl = "";

        public PaymentController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            //baseApiUrl = "http://localhost:1189/api/Account";
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePaymentUrl(CreatePayment model)
        {
            if (TempData["BookingData"] is string bookingData)
            {
                var booking = JsonSerializer.Deserialize<GetBooking>(bookingData);
                // Use 'booking' data as needed in the PaymentController
                model = new CreatePayment
                {
                    UserId = booking.UserId,
                    OrderType = "Booking",
                    RelatiedId = booking.Id,
                    Amount = 50000,
                    Description = "Thanh toán booking",
                };
            }
            var url = _paymentDAO.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {
            var response = _paymentDAO.PaymentExecute(Request.Query);

            return Json(response);
        }
    }
}
