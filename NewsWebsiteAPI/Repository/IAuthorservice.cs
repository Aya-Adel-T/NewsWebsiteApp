using NewsAPI.Models;
using NewsAPI.Repository;

namespace NewsWebsiteAPI.Repository
{
    public interface IAuthorservice :IRepository<Author>
    {
        public List<Author> SortAuthorsByName();
    }
}
