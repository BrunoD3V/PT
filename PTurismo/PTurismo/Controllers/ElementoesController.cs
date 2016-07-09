using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PTurismo.DAL;
using PTurismo.Models;

namespace PTurismo.Controllers
{
    public class ElementoesController : Controller
    {
        private PastoralContext db = new PastoralContext();

        // GET: Elementoes
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PoiSortParm = sortOrder == "Poi" ? "poi_desc" : "Poi";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var elementos = from e in db.Elemento
                       select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                elementos = elementos.Where(e => e.nome.Contains(searchString) || e.poi.nome.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    elementos = elementos.OrderByDescending(e => e.nome);
                    break;
                case "Categoria":
                    elementos = elementos.OrderBy(e => e.poi.nome);
                    break;
                case "categoria_desc":
                    elementos = elementos.OrderByDescending(e => e.poi.nome);
                    break;
                default:
                    elementos = elementos.OrderBy(e => e.nome);
                    break;

            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            elementos = elementos.Include(e => e.poi);

            return View(elementos.ToPagedList(pageNumber, pageSize));
        }

        // GET: Elementoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elemento elemento = db.Elemento.Find(id);
            if (elemento == null)
            {
                return HttpNotFound();
            }
            return View(elemento);
        }

        // GET: Elementoes/Create
        public ActionResult Create()
        {
            ViewBag.PoiID = new SelectList(db.Poi, "PoiID", "nome");
            return View();
        }

        // POST: Elementoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PoiID,nome,descricao,imagem")] Elemento elemento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Elemento.Add(elemento);
                    db.SaveChanges();
                    return RedirectToAction("Create","GaleriaElementoes");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Erro a guardar alterações. Tente outra vez, se o problem persistir contacte o administrador do sitema.");
            }

            ViewBag.PoiID = new SelectList(db.Poi, "PoiID", "nome", elemento.PoiID);
            return View(elemento);
        }

        // GET: Elementoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elemento elemento = db.Elemento.Find(id);
            if (elemento == null)
            {
                return HttpNotFound();
            }
            ViewBag.PoiID = new SelectList(db.Poi, "PoiID", "nome", elemento.PoiID);
            return View(elemento);
        }

        // POST: Elementoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ElementoID,PoiID,nome,descricao,imagem")] Elemento elemento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(elemento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PoiID = new SelectList(db.Poi, "PoiID", "nome", elemento.PoiID);
            return View(elemento);
        }

        // GET: Elementoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elemento elemento = db.Elemento.Find(id);
            if (elemento == null)
            {
                return HttpNotFound();
            }
            return View(elemento);
        }

        // POST: Elementoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Elemento elemento = db.Elemento.Find(id);
            db.Elemento.Remove(elemento);
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
