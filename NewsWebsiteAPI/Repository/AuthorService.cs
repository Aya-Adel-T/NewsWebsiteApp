using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;
using NewsWebsiteAPI.Data;
using NewsWebsiteAPI.Repository;

namespace NewsAPI.Repository
{
    public class AuthorService :BaseRepoService, IAuthorservice
    {
        public AuthorService(IDbContextFactory<ElDbContext> context):base(context)
        {
            
        }
        public List<Author> GetAll()
        {
            List<Author> OrdersList = new();
            using (var customContext = Context.CreateDbContext())
            {
                OrdersList = customContext.Authors.ToList();
            }
           
            return OrdersList;
        }

        public Author? GetDetails(int id)
        {
            var AuthorDetails = new Author();
            using (var customContext = Context.CreateDbContext())
            {
                AuthorDetails = customContext.Authors.Find(id);
            }
            using (var customContext = Context.CreateDbContext())
            {
                AuthorDetails.NewsList = customContext.News.Where(f => f.AuthorID == id).ToList();
            }
            return AuthorDetails;
        }

        public void Insert(Author author)
        {
            using var customContext = Context.CreateDbContext();
            customContext.Authors.Add(author);
            customContext.SaveChanges();
        }

        public Author Delete(int id)
        {
            using var customContext = Context.CreateDbContext();
            Author AuthorData = customContext.Authors.Find(id);
            customContext.Authors.Remove(AuthorData);
            customContext.SaveChanges();
            return AuthorData;
        }

        public void Update(Author author)
        {
            using var customContext = Context.CreateDbContext();
            customContext.Authors.Update(author);
            customContext.SaveChanges();
        }

        public List<Author> SortAuthorsByName()
        {
            List<Author> AuthorsList = new();
            using (var customContext = Context.CreateDbContext())
            {
                AuthorsList = customContext.Authors.OrderBy(n => n.Name).ToList();
            }

            return AuthorsList;

        }
    }
}

