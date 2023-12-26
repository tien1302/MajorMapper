using BAL.DAOs.Interfaces;
using BAL.DTOs.Bookings;
using BAL.DTOs.Payments;
using BAL.DTOs.Slots;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClient.Models;

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

        public async Task<ActionResult> Index()
        {
            Money money = new Money();
            int sum = 0;
            string getyear = Request.Query["year"];
            List<int> years = new List<int>();
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 5; i++)
            {
                years.Add(currentYear - i);
            }
            List<SelectListItem> selectItems = years.Select(y => new SelectListItem { Value = y.ToString(), Text = "Năm "+ y.ToString() }).ToList();
            ViewBag.Years = selectItems;
            if (getyear == null)
            {
            
                HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/listMoney?year={DateTime.Now.Year}");
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<int> listMoney = JsonSerializer.Deserialize<List<int>>(strData, options);

                foreach (var item in listMoney)
                {
                    sum += item;
                }
                money.Sum = sum;
                sum = 0;
                ViewData["Moneys"] = listMoney;
            }
            else
            {
                int year = int.Parse(getyear);
                
                HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/listMoney?year={year}");
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<int> listMoney = JsonSerializer.Deserialize<List<int>>(strData, options);
               
                foreach (var item in listMoney)
                {
                    sum += item;
                }
                money.Sum = sum;
                sum = 0;
                ViewData["Moneys"] = listMoney;

            }

            return View(money);
        }

        public async Task<ActionResult> Dashboard()
        {
            Money money = new Money();
            var id = HttpContext.Session.GetInt32("AccountId");
            int sum = 0;
            int count = 0;
            string getyear = Request.Query["year"];
            List<int> years = new List<int>();
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 5; i++)
            {
                years.Add(currentYear - i);
            }
            List<SelectListItem> selectItems = years.Select(y => new SelectListItem { Value = y.ToString(), Text = "Năm " + y.ToString() }).ToList();
            ViewBag.Years = selectItems;
            if (getyear == null)
            {

                HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/listMoneyById?id={id}&year={DateTime.Now.Year}");
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Tuple<List<int>, List<int>> listMoney = JsonSerializer.Deserialize<Tuple<List<int>, List<int>>>(strData, options);

                foreach (var item in listMoney.Item1)
                {
                    sum += item;
                }
                foreach (var item in listMoney.Item2)
                {
                    count += item;
                }
                money.Sum = sum;
                sum = 0;
                money.Count = count;
                count = 0;
                ViewData["Moneys"] = listMoney.Item1;
                ViewData["Counts"] = listMoney.Item2;
            }
            else
            {
                int year = int.Parse(getyear);

                HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/listMoneyById?id={id}&year={year}");
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Tuple<List<int>, List<int>> listMoney = JsonSerializer.Deserialize<Tuple<List<int>, List<int>>>(strData, options);


                foreach (var item in listMoney.Item1)
                {
                    sum += item;
                }
                foreach (var item in listMoney.Item2)
                {
                    count += item;
                }
                money.Sum = sum;
                sum = 0;
                money.Count = count;
                count = 0;
                ViewData["Moneys"] = listMoney.Item1;
                ViewData["Counts"] = listMoney.Item2;
            }
            
            
            return View(money);
        }


        public async Task<IActionResult> CreatePaymentUrlAsync(GetBooking p)
        {
            CreatePayment model = new CreatePayment()
            {
                PlayerId = p.PlayerId,
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
