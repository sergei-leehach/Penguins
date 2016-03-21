using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteDevelopment.Models;
using SiteDevelopment.Repository;

namespace SiteDevelopment.Controllers
{
    public class GeneratorController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.DropDown = DbQuery.DropDownListGeneration();
            ViewBag.Result = ServiceFunctions.GetResultField();
            ViewBag.Images = DbQuery.GetAllBoards();
            return View();
        }

        [HttpPost]
        public ActionResult Index(InputData data, string result)
        {
            data.AwayTeamShortName = DbQuery.GetShortName(data.AwayTeam);
            data.HomeTeamShortName = DbQuery.GetShortName(data.HomeTeam);
            data.HomeTeamLogo = SetTeamLogo(data.HomeTeamShortName);
            data.AwayTeamLogo = SetTeamLogo(data.AwayTeamShortName);


            ImageGeneration.ImageProcessing(data);

            return View();
        }

        [HttpPost]
        public ActionResult GetPlace(string name)
        {
            string[] array = DbQuery.GetPlace(name);

            return Json(new { city = array[0], arena = array[1] }, JsonRequestBehavior.AllowGet);
        }

        public string SetTeamLogo(string shortName)
        {
            var path = $@"/Images/Logos/{shortName}.png";

            string result;
            if (Server == null)
                result = Directory.GetCurrentDirectory() + path;
            else
                result = Server.MapPath(path);

            return result;
        }
    }
}