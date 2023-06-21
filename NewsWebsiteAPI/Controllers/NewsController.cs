using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;
using NewsAPI.Repository;
using NewsWebsiteAPI.Repository;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        public INewsService NewsRepo { get; set; }
        public NewsController(INewsService newsRepo)
        {
            NewsRepo = newsRepo;
        }

        // GET: api/Authors
        [HttpGet]
        public ActionResult<List<News>> GetNews()
        {

            return NewsRepo.GetAll();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetById(int id)
        {
            return NewsRepo.GetDetails(id);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult PutAuthor(News news)
        {
            if (news.Id != 0 && news != null)
            {
                NewsRepo.Update(news);
                return Ok(news);
            }
            return NotFound();
        }

        // POST: api/News
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<News>> PostNews(News news)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    NewsRepo.Insert(news);
                    return Created("url", news);

                    // return 201 & Url is the place where you added the object
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message); // Return 400!
                }
            }
            return BadRequest();
        }

        // DELETE: api/News/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            News OrderData = NewsRepo.Delete(id);
            return Ok(OrderData);
        }

        //Upload Images
        [HttpPost("uploadImage/{newsTitle}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult UploadImage(IFormFile file, string newsTitle)
        {
            var Results = NewsRepo.UploadImage(file, newsTitle);
            return Ok(Results);
        }

        [HttpGet("filterbyAuthor/{AuthorID}")]
        public ActionResult<List<News>> FilterNewsByAuthor(int AuthorID)
        {

            return NewsRepo.FilterNewsByAuthor(AuthorID);
        }
        [HttpGet("newsByPublicationDate")]

        public ActionResult<List<News>> SortnewsByPublicationDate()
        {

            return NewsRepo.SortNewsByPublicationDate();
        }

    }
}
