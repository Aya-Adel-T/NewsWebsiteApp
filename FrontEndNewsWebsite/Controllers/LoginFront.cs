using FeliveryAdminPanel.Helpers;
using Microsoft.AspNetCore.Authentication;
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
                dfd.Name = "ay7aga";
                TempData["Token"] = session;
            }

            //AuthenticationProperties options = new AuthenticationProperties();

            //options.AllowRefresh = true;
            //options.IsPersistent = true;
            //options.ExpiresUtc = DateTime.UtcNow.AddSeconds(int.Parse(token.expires_in));

            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.Name, model.EmailAddress),
            //        new Claim("AcessToken", string.Format("Bearer {0}", token.access_token)),
            //    };

            //var identity = new ClaimsIdentity(claims, "ApplicationCookie");

            //Request.GetOwinContext().Authentication.SignIn(options, identity);



            if (res.IsSuccessStatusCode)
            {
                //RedirectToAction("Index");
                return View("AdminPanel");
            }
            return View();
        }
    }
}
