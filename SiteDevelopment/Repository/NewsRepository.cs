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

        public News GetNews(int id)
        {
            var news = _db.News.Where(x => x.NewsId == id).ToList();

            if (news.Any())
            {
                return news.FirstOrDefault();
            }
            return null;
        }

        public List<User> GetAuthor()
        {
            var author = (from user in _db.Users where user.UserId == 1 select user).ToList();
            return author;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}