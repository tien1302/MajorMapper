﻿using BAL.DTOs.Accounts;
using BAL.DTOs.Feedbacks;
using BAL.DTOs.TestResults;
using DAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient client;
        private string baseApiUrl = "";
        private string testApiUrl = "";
        private string feedbackApiUrl = "";
        public AccountController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            baseApiUrl = "http://localhost:1189/api/Account";
            feedbackApiUrl = "http://localhost:1189/api/Feedback";

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var accessToken = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //Account
                HttpResponseMessage response = await client.GetAsync(baseApiUrl);
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<GetAccount> list = JsonSerializer.Deserialize<List<GetAccount>>(strData, options);
                return View(list);
            }
            catch (Exception ex)
            {
                return Redirect("~/Home/Index");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            //Token
            var accessToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            int currentId = (int)HttpContext.Session.GetInt32("AccountId");
            string name = HttpContext.Session.GetString("Name");
            if (currentId != id && name != "admin")
            {
                return RedirectToAction("Details", new { id = currentId });
            }
            //Account
            HttpResponseMessage response = await client.GetAsync(baseApiUrl);
            var strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetAccount> list = JsonSerializer.Deserialize<List<GetAccount>>(strData, options);
            GetAccount account = list.Where(p => p.id == id).First();
            //Feedback
            HttpResponseMessage feedbackResponse = await client.GetAsync($"{feedbackApiUrl}/{id}");
            var feedbackData = await feedbackResponse.Content.ReadAsStringAsync();

            var optionF = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetFeedback> listFeedback = JsonSerializer.Deserialize<List<GetFeedback>>(feedbackData, optionF);
            ViewBag.Feedbacks = listFeedback;
            //TestResult
            HttpResponseMessage testResultResponse = await client.GetAsync($"{baseApiUrl}/GetTestResult/{id}");
            var testResultData = await testResultResponse.Content.ReadAsStringAsync();

            var optionT = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<GetTestResult> listTestResult = JsonSerializer.Deserialize<List<GetTestResult>>(testResultData, optionT);
            ViewBag.TestResults = listTestResult;
            return View(account);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAccount p)
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
            UpdateAccount account = JsonSerializer.Deserialize<UpdateAccount>(strData, options);
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id,UpdateAccount p)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(p);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/{id}", contentData);
                if (response.IsSuccessStatusCode)
                {
                    HttpResponseMessage response1 = await client.GetAsync($"{baseApiUrl}/{id}");
                    var token = await response1.Content.ReadAsStringAsync();
                    GetAccount tokenResponse = JsonSerializer.Deserialize<GetAccount>(token);
                    HttpContext.Session.SetString("Name", tokenResponse.name);
                    ViewBag.Message = "Insert successfully!";
                }
                else
                {
                    ViewBag.Message = "Error while calling WebAPI!";
                }
            }
            else
                ViewBag.Message = "Error!";
            return RedirectToAction(nameof(Details));
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
        public async Task<ActionResult> ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassword p)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(p);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/ResetPassword", contentData);
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
            return View("ResetPassword");
        }

    }
}
