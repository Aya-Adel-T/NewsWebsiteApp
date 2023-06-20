using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;
using NewsAPI.Repository;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AuthorsController : ControllerBase
    {
        public IRepository<Author> AuthorRepo { get; set; }
        public AuthorsController(IRepository<Author> authorRepo)
        {
            AuthorRepo = authorRepo;
        }

        // GET: api/Authors
        [HttpGet]
        public ActionResult<List<Author>> GetAuthors()
        {

            return AuthorRepo.GetAll();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetById(int id)
        {
            return AuthorRepo.GetDetails(id);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public  ActionResult PutAuthor(Author author)
        {
            if (author.Id != 0 && author != null)
            {
                AuthorRepo.Update(author);
                return Ok(author);
            }
            return NotFound();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AuthorRepo.Insert(author);
                    return Created("url", author);
                    // return 201 & Url is the place where you added the object
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message); // Return 400!
                }
            }
            return BadRequest();
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            Author OrderData = AuthorRepo.Delete(id);
            return Ok(OrderData);
        }
    }
}
