using FeliveryAdminPanel.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsAPI.Models;
using Newtonsoft.Json;

namespace FrontEndNewsWebsite.Controllers
{
    public class NewsFront : Controller
    {
        APIClient _api = new APIClient();
        public async Task<IActionResult> Index()
        {
            HttpClient Client = _api.Initial();
            try
            {
                var NewsList = await Client.GetFromJsonAsync<List<News>>("api/News");
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
        public async Task<IActionResult> Create(int id)
        {
            HttpClient client = _api.Initial();
            //Authors drop down list
            var AuthorList = await client.GetFromJsonAsync<List<Author>>("api/Authors");
            SelectList AuthorsSelectList = new SelectList(AuthorList, "Id", "Name");

            ViewBag.AuthorList = AuthorsSelectList;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(News news)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.PostAsJsonAsync($"api/News", news);
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<ActionResult> Edit(int id)
        {
            News news = new News();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/News/{id}");
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                news = JsonConvert.DeserializeObject<News>(data);
            }
            //Authors drop down list
            var AuthorList = await client.GetFromJsonAsync<List<Author>>("api/Authors");
            SelectList AuthorsSelectList = new SelectList(AuthorList, "Id", "Name");

            ViewBag.AuthorList = AuthorsSelectList;
            return View(news);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(int id, News news)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.PutAsJsonAsync<News>("api/News/{id}", news);

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }

            return View(news);
        }
        public async Task<ActionResult> Delete(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.DeleteAsync($"api/News/{id}");
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FilterByAuthor(int Author)
        {
            HttpClient Client = _api.Initial();
            try
            {
                var NewsList = await Client.GetFromJsonAsync<List<News>>($"api/News/filterbyAuthor/{Author}");
                return View(NewsList);
            }
            catch (Exception e)
            {
                return View();
            }

        }
        [HttpGet]
        public async Task<IActionResult> SortByPublicationDate()
        {
            HttpClient Client = _api.Initial();
            try
            {
                var NewsList = await Client.GetFromJsonAsync<List<News>>($"api/News/newsByPublicationDate");
                return View(NewsList);
            }
            catch (Exception e)
            {
                return View();
            }

        }
    }
}
