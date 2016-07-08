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

namespace PTurismo.Controllers
{
    public class GaleriaPoisController : Controller
    {
        private PastoralContext db = new PastoralContext();

        // GET: GaleriaPois
        public ActionResult Index()
        {
            var galeriaPoi = db.GaleriaPoi.Include(g => g.Poi);
            return View(galeriaPoi.ToList());
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
