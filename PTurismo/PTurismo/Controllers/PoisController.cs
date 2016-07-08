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
using System.Data.Entity.Infrastructure;

namespace PTurismo.Controllers
{
    public class PoisController : Controller
    {
        private PastoralContext db = new PastoralContext();

        // GET: Pois
        public ActionResult Index()
        {
            var poi = db.Poi.Include(p => p.categoria);
            return View(poi.ToList());
        }

        // GET: Pois/Details/5
        public ActionResult Details(int? id)
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

        // GET: Pois/Create
        public ActionResult Create()
        {
            PopulateCategoriasDropDownList();
            return View();
        }

        // POST: Pois/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PoiID,CategoriaID,nome,latitude,longitude,descricao,imagem,resumo")] Poi poi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Poi.Add(poi);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Erro ao guardar as alterações. Tente novamente, se persistir contacte o administrador.");
            }

            PopulateCategoriasDropDownList(poi.CategoriaID);
            return View(poi);
        }

        // GET: Pois/Edit/5
        public ActionResult Edit(int? id)
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
            PopulateCategoriasDropDownList(poi.CategoriaID);
            return View(poi);
        }

        // POST: Pois/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var poiToUpdate = db.Poi.Find(id);
            if(TryUpdateModel(poiToUpdate,"", new string[] { "nome", "latitude", "longitude", "imagem", "resumo", "categoriaID" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Erro ao guardar as alterações. Tente novamente, se persistir contacte o administrador.");
                }
            }
            PopulateCategoriasDropDownList(poiToUpdate.CategoriaID);
            return View(poiToUpdate);
        }

        // GET: Pois/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Pois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Poi poi = db.Poi.Find(id);
            db.Poi.Remove(poi);
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

        private void PopulateCategoriasDropDownList(object selectedCategoria = null)
        {
            var categoriasQuery = from c in db.Categoria
                                  orderby c.nome
                                  select c;
            ViewBag.CategoriaID = new SelectList(categoriasQuery, "CategoriaID", "Nome", selectedCategoria);
        }
    }
}
