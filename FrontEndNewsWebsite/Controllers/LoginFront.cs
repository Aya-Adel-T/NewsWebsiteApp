using FeliveryAdminPanel.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;
using NuGet.Common;
using NuGet.Protocol;
using System.Data;
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
                ClaimsIdentity claims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                claims.AddClaim(new Claim(ClaimTypes.Email, admin.Email));
                claims.AddClaim(new Claim(ClaimTypes.Name, admin.Email));
                claims.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

                ClaimsPrincipal principal = new ClaimsPrincipal(claims);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            }
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminPanel");
            }
            return View();
        }
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Admin")]

        public IActionResult AdminPanel()
        {
            return View("AdminPanel");
        }

    }
}
