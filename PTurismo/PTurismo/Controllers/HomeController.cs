using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PTurismo.Models;
using PTurismo.DAL;
using PTurismo.ViewModels;


namespace PTurismo.Controllers
{
    public class HomeController : Controller
    {

        private PastoralContext db = new PastoralContext();
        public ActionResult Index(bool? fitToMarkersBounds, bool? clickable, bool? draggable)
        {
            /*var data = from p in db.Poi
                select p;*/

            this.ViewData["FitToMarkersBounds"] = fitToMarkersBounds ?? true;
            this.ViewData["clickable"] = clickable ?? true;
            this.ViewData["draggable"] = draggable ?? true;
          //  ViewData["Pois"] = data;
            return View(new MapaViewModel());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(bool? fitToMarkersBounds, bool? clickable, bool? draggable)
        {
            this.ViewData["FitToMarkersBounds"] = fitToMarkersBounds ?? true;
            this.ViewData["clickable"] = clickable ?? true;
            this.ViewData["draggable"] = draggable ?? true;

            return this.View();
        }
       
        public ActionResult Poi(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poi poi = db.Poi.Find(id);
            if (poi == null)
            {
                return HttpNotFound();
            }
            return View(poi);
        }
    }
}