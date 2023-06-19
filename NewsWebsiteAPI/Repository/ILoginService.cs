using NewsAPI.Models;
using NewsWebsiteAPI.Models.Authentication.Login;

namespace NewsAPI.Repository
{
    public interface ILoginService
    {
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);

    }
}
