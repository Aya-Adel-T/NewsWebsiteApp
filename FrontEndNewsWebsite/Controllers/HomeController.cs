using FeliveryAdminPanel.Helpers;
using FrontEndNewsWebsite.Models;
using Microsoft.AspNetCore.Mvc;
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
                var CategoryList = await Client.GetFromJsonAsync<List<News>>("api/News");
                return View(CategoryList);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            News Category = new News();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/News/{id}");
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                Category = JsonConvert.DeserializeObject<News>(data);
            }
            return View(Category);
        }
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

    }
}