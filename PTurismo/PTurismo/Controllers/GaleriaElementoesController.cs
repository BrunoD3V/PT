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
    public class GaleriaElementoesController : Controller
    {
        private PastoralContext db = new PastoralContext();

        // GET: GaleriaElementoes
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TipoMediaSortParm = String.IsNullOrEmpty(sortOrder) ? "tipoMedia_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var galeriasElementos = from g in db.GaleriaElemento
                             select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                galeriasElementos = galeriasElementos.Where(g => g.legenda.Contains(searchString) || g.tipoMedia.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "tipoMedia_desc":
                    galeriasElementos = galeriasElementos.OrderByDescending(g => g.tipoMedia);
                    break;
                default:
                    galeriasElementos = galeriasElementos.OrderBy(g => g.tipoMedia);
                    break;

            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(galeriasElementos.ToPagedList(pageNumber, pageSize));
        }

        // GET: GaleriaElementoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GaleriaElemento galeriaElemento = db.GaleriaElemento.Find(id);
            if (galeriaElemento == null)
            {
                return HttpNotFound();
            }
            return View(galeriaElemento);
        }

        // GET: GaleriaElementoes/Create
        public ActionResult Create()
        {
            ViewBag.ElementoID = new SelectList(db.Elemento, "ElementoID", "nome");
            return View();
        }

        // POST: GaleriaElementoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GaleriaElementoID,ElementoID,media,legenda,tipoMedia")] GaleriaElemento galeriaElemento)
        {
            if (ModelState.IsValid)
            {
                db.GaleriaElemento.Add(galeriaElemento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ElementoID = new SelectList(db.Elemento, "ElementoID", "nome", galeriaElemento.ElementoID);
            return View(galeriaElemento);
        }

        // GET: GaleriaElementoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GaleriaElemento galeriaElemento = db.GaleriaElemento.Find(id);
            if (galeriaElemento == null)
            {
                return HttpNotFound();
            }
            ViewBag.ElementoID = new SelectList(db.Elemento, "ElementoID", "nome", galeriaElemento.ElementoID);
            return View(galeriaElemento);
        }

        // POST: GaleriaElementoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GaleriaElementoID,ElementoID,media,legenda,tipoMedia")] GaleriaElemento galeriaElemento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(galeriaElemento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ElementoID = new SelectList(db.Elemento, "ElementoID", "nome", galeriaElemento.ElementoID);
            return View(galeriaElemento);
        }

        // GET: GaleriaElementoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GaleriaElemento galeriaElemento = db.GaleriaElemento.Find(id);
            if (galeriaElemento == null)
            {
                return HttpNotFound();
            }
            return View(galeriaElemento);
        }

        // POST: GaleriaElementoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GaleriaElemento galeriaElemento = db.GaleriaElemento.Find(id);
            db.GaleriaElemento.Remove(galeriaElemento);
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
