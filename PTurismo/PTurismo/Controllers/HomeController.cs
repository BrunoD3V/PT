using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PTurismo.Models;
using PTurismo.DAL;


namespace PTurismo.Controllers
{
    public class HomeController : Controller
    {

        private PastoralContext db = new PastoralContext();
        public ActionResult Index(bool? fitToMarkersBounds)
        {

          

            var data = from p in db.Poi
                select p;
            this.ViewData["FitToMarkersBounds"] = fitToMarkersBounds ?? true;
            ViewData["Pois"] = data;
            return View(data.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
    }
}