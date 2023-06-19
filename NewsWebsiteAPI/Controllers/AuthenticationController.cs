using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewsWebsiteAPI.Models.Authentication.SignUp;

namespace NewsWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
            private readonly UserManager<IdentityUser> UserManager;
            private readonly RoleManager<IdentityRole> RoleManager;
            private readonly IConfiguration Configuration;
        public AuthenticationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
           UserManager = userManager;
            RoleManager = roleManager;
            Configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, string role)
        {

            //Add user to db
            IdentityUser user = new()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.UserName,
            };
            if (await RoleManager.RoleExistsAsync(role))
            {
                var result = await UserManager.CreateAsync(user, registerUser.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                await UserManager.AddToRoleAsync(user, role);
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }


        }
    }
}
