using FeliveryAdminPanel.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsAPI.Models;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace FrontEndNewsWebsite.Controllers
{
    public class NewsFront : Controller
    {   
        APIClient _api = new APIClient();
        public async Task<IActionResult> Index()
        {
            var token = TempData["Token"];
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
            var token = TempData["Token"];
            News news = new News();
            HttpClient client = _api.Initial();
            var token1 = token.ToString();
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
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
            var token = TempData["Token"];
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
            var token = ViewData["Token"];
            HttpClient client = _api.Initial();
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.ToString());
            HttpResponseMessage res = await client.PostAsJsonAsync($"api/News", news);
            if (res.IsSuccessStatusCode)
            {
                return View("addNewsImage");
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
            //var token = TempData["Token"];
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.ToString());
            HttpResponseMessage res = await client.PutAsJsonAsync<News>("api/News/{id}", news);

            if (res.IsSuccessStatusCode)
            {
                return View("addNewsImage");
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
            //var token = TempData["Token"];
            //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.ToString());
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
        [HttpPost]
        public async Task<IActionResult> addNewsImage(IFormFile file, string Title)
        {
            HttpClient client = _api.Initial();
 
                using (var content = new MultipartFormDataContent())
                {
                content.Add(new StreamContent(file.OpenReadStream())
                {
                    Headers =
                     {
                         ContentLength = file.Length,
                         ContentType = new MediaTypeHeaderValue(file.ContentType)
                     }
                }, "file", file.FileName);

                HttpResponseMessage res = await client.PostAsync($"api/News/uploadImage/{Title}", content);
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    return View();
                }   
        }
    }
}
