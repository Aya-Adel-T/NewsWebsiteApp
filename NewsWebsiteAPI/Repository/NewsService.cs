using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;
using NewsWebsiteAPI.Data;
using NewsWebsiteAPI.Repository;
using System.Security.Cryptography.X509Certificates;

namespace NewsAPI.Repository
{
    public class NewsService
     : BaseRepoService, INewsService
    {
        private readonly IWebHostEnvironment _environment;
        public NewsService(IDbContextFactory<ElDbContext> context ,IWebHostEnvironment environment) : base(context)
        {
            _environment = environment;
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

        public string UploadImage(IFormFile Img, string title)
        {
            using (var customContext = Context.CreateDbContext())
            {
                if (customContext.News.Where(r => r.Title == title).First() == null)
                    throw new Exception("Store Name Not Found");
            }
            if (Img != null)
            {
                string ImageUrl = string.Empty;
                //string HostUrl = "https://localhost:7150/";
                string HostUrl = "file:///D:/News%20Website/TrialManulaJWT/NewsWebsiteApp/NewsWebsiteAPI/wwwroot/";

                string RawName = title.Replace(" ", "-");
                string filePath = _environment.WebRootPath + "\\Uploads\\Product\\" + RawName;
                string imagepath = filePath + "\\newsImg.png";
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                if (Directory.Exists(imagepath))
                    Directory.Delete(imagepath);
                using (FileStream fileStream = File.Create(imagepath))
                {
                    Img.CopyTo(fileStream);
                }
                ImageUrl = HostUrl + "/uploads/Product/" + RawName + "/newsImg.png";
                using (var customContext = Context.CreateDbContext())
                {
                    var storeImg = customContext.News.Where(r => r.Title == title).First();
                    storeImg.NewsImg = ImageUrl;
                    customContext.SaveChanges();
                }
                return ImageUrl;
            }
            else
                throw new Exception("Image Not Found");
        }

        public List<News> FilterNewsByAuthor(int AuthorID)
        {
            List<News> newsList = new();
          
            using (var customContext = Context.CreateDbContext())
            {
                foreach (var item in newsList)
                {
                    item.Author = customContext.Authors.First(r => r.Id == item.AuthorID);

                }
            }
            using (var customContext = Context.CreateDbContext())
            {
                newsList = customContext.News.Where(o => o.AuthorID == AuthorID).ToList();
            }
            return newsList;
        }

        public List<News> SortNewsByPublicationDate()
        {
          
                List<News> newsList = new();
                using (var customContext = Context.CreateDbContext())
                {
                    newsList = customContext.News.OrderBy(n=>n.PublicationDate).ToList();
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
    }
}
