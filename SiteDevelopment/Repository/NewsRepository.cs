using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SiteDevelopment.Models;

namespace SiteDevelopment.Repository
{
    public class NewsRepository : BaseRepository
    {
        public NewsRepository(string connectionString) : base(connectionString)
        {
        }

        public List<News> GetNews()
        {
            var newsCollection = new List<News>();
            var sqlQuery = "Select * from News";
            //var news = _db.News.Include(c => c.Bundle.Select(x => x.Tag)).ToList();
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);

           using (command)
            {
                command.Connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var news = new News(reader);
                    newsCollection.Add(news);
                }
                command.Connection.Close();
            }
            return newsCollection;
        }

        public News GetNewsById(int id)
        {
            var news = new News();
            var sqlQuery = $"Select * from News where NewsId = {id}";
            //var news = _db.News.Where(x => x.NewsId == id).Include(c => c.Bundle.Select(x => x.Tag)).ToList();
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                news = new News(reader);
            }
            command.Connection.Close();
            return news;
        }

        public List<News> GetNewsByTagId(int id)
        {
            var news = new List<News>();
            var sqlQuery = $"Select * from Tags where TagId = {id}";
            //var coll = _db.News.SelectMany(b => b.Bundle.Where(t => t.TagId == tagId).Select(x => x.News)).Include(c => c.Bundle.Select(x => x.Tag)).ToList();
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var n = new News(reader);
                news.Add(n);
            }
            command.Connection.Close();
            return news;
        }

        public void CreateNews(News news)
        {
            var sqlQuery = $"Insert into News (Image, Title, MainText, PublicationTime, UserId) Values ({news.Image}, {news.Title}, {news.MainText}, {news.PublicationTime}, {news.UserId})";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public void DeleteNews(int id)
        {
            var sqlQuery = $"Delete from News where NewsId = {id}";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            DbQuery.ExecuteCommand(command);
        }

        public List<Tag> GetTags()
        {
            var tags = new List<Tag>();
            var sqlQuery = "Select * from Tags";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Tag tag = new Tag
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Title = reader["Title"].ToString()
                };
                tags.Add(tag);
            }
            command.Connection.Close();
            return tags;
        }

        public Comment CreateComment(Comment comment)
        {
            UserRepository repository = new UserRepository(DbQuery.ConnectionString);
            comment.Author = repository.GetUserById(comment.UserId);
            comment.PublicationTime = DateTime.Now;

            var sqlQuery =
                $"Insert into Comments (Text, PublicationTime, NewsId, UserId) Values ({comment.Text}, {comment.PublicationTime}, {comment.NewsId}, {comment.UserId})";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();

            return comment;
        }

        public List<Comment> GetComments(News news)
        {
            var comments = new List<Comment>();
            var sqlQuery = $"Select * from Comments where NewsId = {news.NewsId}";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var comment = new Comment
                {
                    CommentId = Convert.ToInt32(reader["CommentId"]),
                    Text = reader["Text"].ToString(),
                    Like = Convert.ToInt32(reader["[Like]"]),
                    Dislike = Convert.ToInt32(reader["Dislike"]),
                    PublicationTime = Convert.ToDateTime(reader["PublicationTime"]),
                    NewsId = Convert.ToInt32(reader["NewsId"]),
                    UserId = Convert.ToInt32(reader["UserId"])
                };
                comments.Add(comment);
            }
            command.Connection.Close();
            return comments;
        }       
    }
}