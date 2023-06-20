using FeliveryAdminPanel.Helpers;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;

namespace FrontEndNewsWebsite.Controllers
{
    public class AuthorsFront : Controller
    {
        APIClient _api = new APIClient();
        public async Task<IActionResult> Index()
        {
            HttpClient Client = _api.Initial();
            try
            {
                var CategoryList = await Client.GetFromJsonAsync<List<Author>>("api/Authors");
                return View(CategoryList);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}
