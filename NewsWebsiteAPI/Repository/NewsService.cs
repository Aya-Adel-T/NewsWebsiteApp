using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;
using NewsWebsiteAPI.Data;

namespace NewsAPI.Repository
{
    public class NewsService
     : BaseRepoService, IRepository<News>
    {
        public NewsService(IDbContextFactory<ElDbContext> context) : base(context)
        {

        }
        public List<News> GetAll()
        {
            List<News> newsList = new();
            using (var customContext = Context.CreateDbContext())
            {
                newsList = customContext.News.ToList();
            }
            using (var customContext = Context.CreateDbContext())
            {
                foreach (var item in newsList)
                {
                    item.Author = customContext.Authors.First(r => r.Id == item.AuthorID);

                }
            }

            return newsList;
        }

        public News? GetDetails(int id)
        {
            var newsDetails = new News();
            using (var customContext = Context.CreateDbContext())
            {
                newsDetails = customContext.News.Find(id);
            }
            using (var customContext = Context.CreateDbContext())
            {

                newsDetails.Author = customContext.Authors.First(r => r.Id == newsDetails.AuthorID);

            }
            return newsDetails;
        }

        public void Insert(News news)
        {
            using var customContext = Context.CreateDbContext();
            news.CreationDate = DateTime.Now;
            customContext.News.Add(news);
            customContext.SaveChanges();
        }

        public News Delete(int id)
        {
            using var customContext = Context.CreateDbContext();
            News newsData = customContext.News.Find(id);
            customContext.News.Remove(newsData);
            customContext.SaveChanges();
            return newsData;
        }

        public void Update(News news)
        {
            using var customContext = Context.CreateDbContext();
            customContext.News.Update(news);
            customContext.SaveChanges();
        }
    }
}
