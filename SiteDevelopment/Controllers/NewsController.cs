﻿using System;
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
        // GET: News
        public ActionResult Index()
        {
            var news = _db.GetAllNews();
            return View(news);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult AddIndex()
        {
            return PartialView("AddIndex");
        }

        public ActionResult News(int id)
        {
            var news = _db.GetNews(id);
            return View(news);
        }
        
        [HttpPost]
        public ActionResult Create(News news)
        {
            news.PublicationTime = DateTime.Now;
            var author = _db.GetAuthor().ToArray();
            news.Author = author[0];
            _db.CreateNews(news);
            return RedirectToAction("Index");
        }
    }
}