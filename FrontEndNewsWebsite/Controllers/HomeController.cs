using FeliveryAdminPanel.Helpers;
using FrontEndNewsWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsAPI.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace FrontEndNewsWebsite.Controllers
{
    public class HomeController : Controller
    {

        APIClient _api = new APIClient();
        public async Task<IActionResult> Index()
        {
            HttpClient Client = _api.Initial();
            try
            {
                var NewsList = await Client.GetFromJsonAsync<List<News>>("api/News");

                //Authors drop down list
                var AuthorList = await Client.GetFromJsonAsync<List<Author>>("api/Authors");
                SelectList AuthorsSelectList = new SelectList(AuthorList, "Id", "Name");

                ViewBag.AuthorList = AuthorsSelectList;

                return View(NewsList);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            News news = new News();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/News/{id}");
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                news = JsonConvert.DeserializeObject<News>(data);
            }
            return View(news);

        }
        public async Task<IActionResult> About()
        {
           
                return View();
            
        }
        public async Task<IActionResult> ContactUs()
        {

            return View();

        }
    }
}