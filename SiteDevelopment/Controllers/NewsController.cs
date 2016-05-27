using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteDevelopment.Models;
using SiteDevelopment.Repository;

namespace SiteDevelopment.Controllers
{
    public class NewsController : Controller
    {
        NewsRepository _db = new NewsRepository();

        [AllowAnonymous]
        public ActionResult Index(int? tagId)
        {
            if (tagId == null)
            {
                var news = _db.GetAllNews();
                return View(news);
            }
            else
            {
                var news = _db.GetNewsByTagId(tagId);
                return View(news);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.Tags = _db.GetTags();
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(News news)
        {
            news.PublicationTime = DateTime.Now;
            var author = _db.GetAuthor();
            author.RememberMe = false;
            news.Author = author;
            _db.CreateNews(news);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult AddIndex()
        {
            return PartialView("AddIndex");
        }

        [AllowAnonymous]
        public ActionResult News(int id)
        {
            var news = _db.GetNews(id);
            return View(news);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteNews(int newsId)
        {
            _db.DeleteNews(newsId);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Comments(News news)
        {
            var model = _db.GetComments(news);
            return View(model);
        }

        [Authorize]
        public ActionResult CreateComment()
        {
            return PartialView();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateComment(Comment comment)
        {
            var record = _db.CreateComment(comment);
            return PartialView("SingleComment", record);
        }
    }
}