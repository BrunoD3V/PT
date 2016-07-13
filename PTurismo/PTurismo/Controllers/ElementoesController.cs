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
using System.IO;
using System.Data.Entity.Infrastructure;

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
        public ActionResult Create([Bind(Include = "PoiID,nome,descricao,imagem")] Elemento elemento, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string[] allowedImageExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                    String fileExtension = Path.GetExtension(upload.FileName);
                    if (upload != null && upload.ContentLength > 0)
                    {
                        for (int i = 0; i < allowedImageExtensions.Length; i++)
                        {
                            if (fileExtension == allowedImageExtensions[i])
                            {
                                elemento.ImagemElemento = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                                elemento.FileType = FileType.Imagem;
                                upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/GaleriaElemento/Imagem"), elemento.ImagemElemento));
                            }
                        }
                        db.Elemento.Add(elemento);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("",
                    "Erro ao guardar as alterações. Tente novamente, se persistir contacte o administrador.");
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
        public ActionResult Edit([Bind(Include = "ElementoID,PoiID,nome,descricao")] Elemento elemento, HttpPostedFileBase upload)
        {
            var elementoToUpdate = db.Elemento.Find(elemento.ElementoID);
            try
            {
                if (TryUpdateModel(elementoToUpdate, "", new string[] {"ElementoID", "PoiID", "nome", "descricao"}))
                {
                    try
                    {
                        if (upload != null && upload.ContentLength > 0)
                        {
                            string[] allowedImageExtensions = { ".gif", ".png", ".jpeg", ".jpg" };

                            String fileExtension = Path.GetExtension(upload.FileName);
                            foreach (string t in allowedImageExtensions)
                            {
                                if (fileExtension == t)
                                {
                                    var FileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                                    var FileTypes = FileType.Imagem;
                                    elementoToUpdate.ImagemElemento = FileName;
                                    elementoToUpdate.FileType = FileTypes;
                                    
                                    upload.SaveAs(Path.Combine(Server.MapPath("~/Content/GaleriaElemento/Imagem"), FileName));
                                }
                            }
                        }
                    }
                    catch (RetryLimitExceededException)
                    {
                        ModelState.AddModelError("", "Erro ao guardar alterações, tente mais tarde. Se o problema persistir contate o administrador do sistema.");
                    }
                }
            }
            catch (Exception)
            {
                
            }

            if (ModelState.IsValid)
            {
                db.Entry(elementoToUpdate).State = EntityState.Modified;
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
