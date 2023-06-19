using Microsoft.EntityFrameworkCore;
using NewsWebsiteAPI.Data;

namespace NewsAPI.Repository
{
    public class BaseRepoService
    {
        public IDbContextFactory<ElDbContext> Context { get; set; }

        public BaseRepoService(IDbContextFactory<ElDbContext> context)
        {
            Context = context;
        }
    }
}
