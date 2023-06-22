using Azure.Core;
using FeliveryAdminPanel.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsAPI.Models;
using Newtonsoft.Json;
using System.Linq;

namespace FrontEndNewsWebsite.Controllers
{
    public class AuthorsFront : Controller
    {

        //System.Object token= new Object();
     
        APIClient _api = new APIClient();

        public async Task<IActionResult> Index()
        {
           var  token = TempData["Token"];
            //var s = token;
            //ViewData["haga"] = s;
            HttpClient Client = _api.Initial();
            try
            {
                //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.ToString());
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
            var token = ViewData["haga"];


            Author author = new Author();
            HttpClient client = _api.Initial();
        

            try
            {           
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.ToString());
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



        //[HttpGet]
        //public async Task<IActionResult> Details(int id)
        //{
        //    var token = TempData["Token"];

        //    Author author = new Author();
        //    HttpClient client = _api.Initial();
        //    try
        //    {
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.ToString());
        //        HttpResponseMessage res = await client.GetAsync($"api/Authors/{id}");
        //        if (res.IsSuccessStatusCode)
        //        {

        //            string data = res.Content.ReadAsStringAsync().Result;
        //            author = JsonConvert.DeserializeObject<Author>(data);
        //        }
        //    }

        //    catch
        //    {
        //        Redirect("Error");

        //    }
        //    return View(author);
        //}




        public async Task<IActionResult> Create(int id)
        {
            var token = ViewData["haga"];
            HttpClient client = _api.Initial();
         

            //Author SecurityID
            //var authorr = await client.GetAsync($"api/Authors/{id}");
            //var RestaurantsSelectList = (authorr, "Id", "Name");
            //ViewBag.SecurityID = RestaurantsSelectList;


            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            var token = ViewData["haga"];
            HttpClient client = _api.Initial();
           
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.ToString());
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
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.ToString());
            HttpResponseMessage res = await client.PutAsJsonAsync<Author>("api/Authors/{id}", author);

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }

            return View(author);
        }
        public async Task<ActionResult> Delete(int? id)
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var token = ViewData["haga"];
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
