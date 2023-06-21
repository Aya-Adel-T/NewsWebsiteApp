
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;
using NewsAPI.Repository;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        public ILoginService LoginRepo { get; set; }
        public LoginController(ILoginService loginRepo)
        {
            LoginRepo = loginRepo;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await LoginRepo.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            HttpContext.Response.Headers.Add("Token", result.Token);
            return Ok(result);
        }
    }
}
