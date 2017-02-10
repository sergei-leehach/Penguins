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
    //[Authorize(Roles = "admin")]
    public class GeneratorController : Controller
    {
        GeneratorRepository _db = new GeneratorRepository(DbQuery.ConnectionString);  
             
        public ActionResult Index()
        {
            ViewBag.DropDown = _db.DropDownListGeneration();
            ViewBag.Result = ServiceFunctions.GetResultField();
            ViewBag.Images = _db.GetAllBoards();
            return View();
        }

        [HttpPost]
        public ActionResult Index(InputData data)
        {
            data.AwayTeamShortName = _db.GetShortName(data.AwayTeam);
            data.HomeTeamShortName = _db.GetShortName(data.HomeTeam);
            data.HomeTeamLogo = SetTeamLogo(data.HomeTeamShortName);
            data.AwayTeamLogo = SetTeamLogo(data.AwayTeamShortName);
            data.BackgroundImage = Server.MapPath(data.BackgroundImage);

            string preview = @"Images\Posters\Preview\";
            preview = SetPath(preview);
            string recap = @"Images\Posters\Recap\";
            recap = SetPath(recap);
            //data.Result = (TypeOfResult) Enum.Parse(typeof (TypeOfResult), result);

            ImageGeneration.ImageProcessing(data, preview, recap);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetPlace(string name)
        {
            string[] array = _db.GetPlace(name);

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

        public string SetPath(string path)
        {
            string result;
            if (Server == null)
                result = Directory.GetCurrentDirectory() + path;
            else
                result = Server.MapPath(path);

            return result;
        }
    }
}