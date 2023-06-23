using Azure.Core;
using FeliveryAdminPanel.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsAPI.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;

namespace FrontEndNewsWebsite.Controllers
{
    public class AuthorsFront : Controller
    {
        APIClient _api = new APIClient();

        public async Task<IActionResult> Index()
        {
           var  token = TempData["Token"];
            var token1 = TempData["tokentani"];
            ViewData["tokentalt"] = token1;
            HttpClient Client = _api.Initial();
            try
            {
                //var d = HttpContext.User;
                //var t = ((ClaimsIdentity)HttpContext.User.Identity);
                //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer ", token.ToString());
                var AuthorsList = await Client.GetFromJsonAsync<List<Author>>("api/Authors");
                return View(AuthorsList);
            }
            catch (Exception e)
            {
                return View();
            }

        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
          var token1 = TempData["tokentani"];
            Author author = new Author();
            HttpClient client = _api.Initial();
            try
            {
                var t = ((ClaimsIdentity)HttpContext.User.Identity);
 
                HttpResponseMessage res = await client.GetAsync($"api/Authors/{id}");
                if (res.IsSuccessStatusCode)
                {

                    string data = res.Content.ReadAsStringAsync().Result;
                    author = JsonConvert.DeserializeObject<Author>(data);
                }
                return View(author);
            }  
            catch
            {
                Redirect("Error");

            }
            return View(author);
        }

        public async Task<IActionResult> Create(int id)
        {
            var token = ViewData["haga"];
            HttpClient client = _api.Initial();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            var token = ViewData["haga"];
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.PostAsJsonAsync($"api/Authors", author);
            if (res.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<ActionResult> Edit(int id)
        {
            var token = ViewData["haga"];
            Author author = new Author();
            HttpClient client = _api.Initial();
        
            HttpResponseMessage res = await client.GetAsync($"api/Authors/{id}");
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<Author>(data);
            }
            return View(author);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Author author)
        {
            var token = ViewData["haga"];
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.PutAsJsonAsync<Author>("api/Authors/{id}", author);

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }

            return View(author);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            Author author = new Author();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Authors/{id}");
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<Author>(data);
            }
            return View(author);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.DeleteAsync($"api/Authors/{id}");
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> SortAuthorsbyName()
        {
            HttpClient Client = _api.Initial();
            try
            {
                var AuthorsList = await Client.GetFromJsonAsync<List<Author>>($"api/Authors/AuthorsByName");
                return View(AuthorsList);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}
