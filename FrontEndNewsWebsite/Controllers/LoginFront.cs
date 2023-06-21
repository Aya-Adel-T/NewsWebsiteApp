﻿using FeliveryAdminPanel.Helpers;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;

namespace FrontEndNewsWebsite.Controllers
{
    public class LoginFront : Controller
    {
        APIClient _api = new APIClient();
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Index";
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Index(TokenRequestModel admin)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.PostAsJsonAsync($"api/Login/Login", admin);
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
