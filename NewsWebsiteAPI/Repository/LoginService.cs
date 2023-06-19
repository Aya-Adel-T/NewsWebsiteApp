using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewsAPI.Models;
using NewsWebsiteAPI.Data;
using NewsWebsiteAPI.Models.Authentication.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsAPI.Repository
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _IConfiguration;
        public IDbContextFactory<ElDbContext> Context { get; }

        public LoginService(UserManager<IdentityUser> userManager, IConfiguration _IConfig, IDbContextFactory<ElDbContext> context)
        {
            _userManager = userManager;
            _IConfiguration = _IConfig;
            Context = context;
        }
        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }
            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.Id = user.Id;
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();
            return authModel;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(IdentityUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }
            .Union(userClaims)
            .Union(roleClaims);

            var Key = _IConfiguration["JWT:key"];
            var issuer = _IConfiguration["JWT:issuer"];
            var audience = _IConfiguration["JWT:Audience"];
            var dur = _IConfiguration["JWT:DurationInDays"];
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddDays(double.Parse(dur)),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
