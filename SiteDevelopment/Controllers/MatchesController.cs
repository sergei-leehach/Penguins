using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiteDevelopment.Models;
using SiteDevelopment.Repository;

namespace SiteDevelopment.Controllers
{
    public class MatchesController : Controller
    {
        SiteRepository _db = new SiteRepository();

        // GET: Matches
        public ActionResult Index()
        {
            ViewBag.DropDown = _db.GetTeams();
            ViewBag.Result = ServiceFunctions.GetResultField();
            return View();     
        }

        [HttpPost]
        public ActionResult Index(Matches match)
        {
            _db.NhlTableGeneration(match);
            return RedirectToAction("Index", "Standings");
        }
    }
}
