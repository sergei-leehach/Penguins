using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiteDevelopment.Models;
using SiteDevelopment.Repository;

namespace SiteDevelopment.Controllers
{
    public class MatchController : Controller
    {
        TeamsRepository _db = new TeamsRepository(DbQuery.ConnectionString);

        // GET: Matches
        public ActionResult Index()
        {
            ViewBag.DropDown = _db.GetTeams();
            ViewBag.Result = ServiceFunctions.GetResultField();
            return View();     
        }

        [HttpPost]
        public ActionResult Index(Match match)
        {
            //match.Result = (TypeOfResult) Enum.Parse(typeof (TypeOfResult), result); [ModelBinder(typeof(MatchModelBinder))]
            _db.NhlTableGeneration(match);
            return RedirectToAction("Index", "Standings");
        }
    }
}
