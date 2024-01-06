using BAL.DTOs.Accounts;
using BAL.DTOs.Majors;
using BAL.DTOs.PersonalityTypes;
using BAL.DTOs.Questions;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class QuestionController : Controller
    {
        private readonly HttpClient client;
        private string baseApiUrl = "";
        private string personApiUrl = "";
        public QuestionController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = "http://localhost:1189/api/Question";
            personApiUrl = "http://localhost:1189/api/PersonalityType";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(baseApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetQuestion> list = JsonSerializer.Deserialize<List<GetQuestion>>(strData, options);
            return View(list);
        }
        public async Task<IActionResult> Processing()
        {
            HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/GetProcessing");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetQuestion> list = JsonSerializer.Deserialize<List<GetQuestion>>(strData, options);
            return View(list);
        }
        public async Task<ActionResult> Create(int id)
        {
            //Question
            HttpResponseMessage responseq = await client.GetAsync($"{baseApiUrl}/{id}");
            if (responseq.IsSuccessStatusCode)
            {
                var strDataq = await responseq.Content.ReadAsStringAsync();
                var optionsq = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                CreateQuestion question = JsonSerializer.Deserialize<CreateQuestion>(strDataq, optionsq);
                //Personality Type
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
                return View(question);
            }
            else
            {
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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateQuestion p)
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
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id, string status)
        {
            UpdateQuestion question = new UpdateQuestion();
            question.Status = status;
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(question);
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
            return RedirectToAction(nameof(Processing));
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
