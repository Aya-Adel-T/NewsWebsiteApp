﻿using NewsAPI.Models;
using NewsAPI.Repository;

namespace NewsWebsiteAPI.Repository
{
    public interface INewsService :IRepository<News>
    {
        public string UploadImage(IFormFile Img, string StoreName);
        public List<News> FilterNewsByAuthor(int AuthorID);
        public List<News> SortNewsByPublicationDate();



    }
}
