using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiteDevelopment.Models;
using SiteDevelopment.Repository;

namespace SiteDevelopment.Controllers
{
    public class StandingsController : Controller
    {
        TeamsRepository _db = new TeamsRepository();

        // GET: Standings
        [AllowAnonymous]
        public ActionResult Index()
        {
            var league = _db.GetLeague();
            if (league.Count() == 0)
            {
                _db.CreateStandings();
                //league = _db.GetLeague();
            }
            return View(league);
        }
    }
}
