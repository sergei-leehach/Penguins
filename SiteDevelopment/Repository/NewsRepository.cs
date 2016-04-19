using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<News> GetAllNews()
        {
            var news = _db.News.Include(c => c.Bundle.Select(x => x.Tag)).ToList();
            return news;
        }

        public IEnumerable<Tag> GetTags()
        {
            var tags = _db.Tags.ToList();
            return tags;
        }

        public void CreateNews(News news)
        {
            _db.News.Add(news);
            _db.SaveChanges();
        }

        public News GetNews(int id)
        {
            var news = _db.News.Where(x => x.NewsId == id).Include(c => c.Bundle.Select(x => x.Tag)).ToList();

            if (news.Any())
            {
                return news.FirstOrDefault();
            }
            return null;
        }

        public List<News> GetNewsByTagId(int? tagId)
        {
            var coll = _db.News.SelectMany(b => b.Bundle.Where(t => t.TagId == tagId).Select(x => x.News)).Include(c => c.Bundle.Select(x => x.Tag)).ToList();
            return coll;
        }

        public List<User> GetAuthor()
        {
            var author = (from user in _db.Users where user.UserId == 1 select user).ToList();
            return author;
        }

        public List<Comment> GetComments(News news)
        {
            var comments = (from x in _db.Comments where x.NewsId == news.NewsId select x).ToList();

            return comments;
        }

        public void DeleteNews(int newsId)
        {
            var news = (from n in _db.News where n.NewsId == newsId select n).FirstOrDefault();
            _db.News.Attach(news);
            _db.News.Remove(news);
            _db.SaveChanges();
        }

        public Comment CreateComment(Comment comment)
        {
            comment.Author = _db.Users.Find(1);
            comment.PublicationTime = DateTime.Now;
            _db.Comments.Add(comment);
            _db.SaveChanges();

            return comment;
        } 

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}