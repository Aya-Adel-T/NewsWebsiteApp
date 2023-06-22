using FeliveryAdminPanel.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;
using NuGet.Common;
using NuGet.Protocol;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;

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
            res.ToJson();

            HttpHeaders headers = res.Headers;
            IEnumerable<string> values;
            if (headers.TryGetValues("Token", out values))
            {          
                string session = values.First();
                Cookie dfd = new Cookie();
                dfd.Value = session;
                TempData["Token"] = session;
                ViewData["tokenbag"] = session;

                var identity = new ClaimsIdentity(new List<Claim>
                 {
                     new Claim("token", session, ClaimValueTypes.String)
                 }, "Custom");
                HttpContext.User = new ClaimsPrincipal(identity);
                var t = ((ClaimsIdentity)HttpContext.User.Identity);
            }
            if (res.IsSuccessStatusCode)
            {
                //RedirectToAction("Index");
                return View("AdminPanel");
            }
            return View();
        }
    }
}
