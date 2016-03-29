using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteDevelopment.Models;

namespace SiteDevelopment.Repository
{
    public class NewsRepository : IDisposable
    {
        private NewsContext _db;

        public NewsRepository()
        {
            _db = new NewsContext();
        }

        public IEnumerable<News> GetAllNews()
        {
            return _db.News.ToList();
        }

        public void CreateNews(News news)
        {
            _db.News.Add(news);
            _db.SaveChanges();
        } 

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}