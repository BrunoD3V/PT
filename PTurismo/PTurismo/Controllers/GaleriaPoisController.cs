using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PTurismo.DAL;
using PTurismo.Models;
using PagedList;

namespace PTurismo.Controllers
{
    public class GaleriaPoisController : Controller
    {
        private PastoralContext db = new PastoralContext();

        // GET: GaleriaPois
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TipoMediaSortParm = String.IsNullOrEmpty(sortOrder) ? "tipoMedia_desc" : "";
            ViewBag.LegendaSortParm = sortOrder == "Legenda" ? "legenda_desc" : "Legenda";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var galeriasPois = from g in db.GaleriaPoi
                               select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                galeriasPois = galeriasPois.Where(g => g.legenda.Contains(searchString) || g.tipoMedia.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "tipoMedia_desc":
                    galeriasPois = galeriasPois.OrderByDescending(g => g.tipoMedia);
                    break;
                case "Legenda":
                    galeriasPois = galeriasPois.OrderBy(c => c.legenda);
                    break;
                case "legenda_desc":
                    galeriasPois = galeriasPois.OrderByDescending(c => c.legenda);
                    break;
                default:
                    galeriasPois = galeriasPois.OrderBy(g => g.tipoMedia);
                    break;

            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            galeriasPois = galeriasPois.Include(g => g.Poi);

            return View(galeriasPois.ToPagedList(pageNumber, pageSize));
        }

        // GET: GaleriaPois/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GaleriaPoi galeriaPoi = db.GaleriaPoi.Find(id);
            if (galeriaPoi == null)
            {
                return HttpNotFound();
            }
            return View(galeriaPoi);
        }

        // GET: GaleriaPois/Create
        public ActionResult Create()
        {
            ViewBag.PoiID = new SelectList(db.Poi, "PoiID", "nome");
            return View();
        }

        // POST: GaleriaPois/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GaleriaPoiID,PoiID,media,legenda,tipoMedia")] GaleriaPoi galeriaPoi)
        {
            if (ModelState.IsValid)
            {
                db.GaleriaPoi.Add(galeriaPoi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PoiID = new SelectList(db.Poi, "PoiID", "nome", galeriaPoi.PoiID);
            return View(galeriaPoi);
        }

        // GET: GaleriaPois/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GaleriaPoi galeriaPoi = db.GaleriaPoi.Find(id);
            if (galeriaPoi == null)
            {
                return HttpNotFound();
            }
            ViewBag.PoiID = new SelectList(db.Poi, "PoiID", "nome", galeriaPoi.PoiID);
            return View(galeriaPoi);
        }

        // POST: GaleriaPois/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GaleriaPoiID,PoiID,media,legenda,tipoMedia")] GaleriaPoi galeriaPoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(galeriaPoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PoiID = new SelectList(db.Poi, "PoiID", "nome", galeriaPoi.PoiID);
            return View(galeriaPoi);
        }

        // GET: GaleriaPois/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GaleriaPoi galeriaPoi = db.GaleriaPoi.Find(id);
            if (galeriaPoi == null)
            {
                return HttpNotFound();
            }
            return View(galeriaPoi);
        }

        // POST: GaleriaPois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GaleriaPoi galeriaPoi = db.GaleriaPoi.Find(id);
            db.GaleriaPoi.Remove(galeriaPoi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
