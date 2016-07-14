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
using System.IO;
using PagedList;

namespace PTurismo.Controllers
{
    public class PoisController : Controller
    {
        private PastoralContext db = new PastoralContext();

        // GET: Pois
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CategoriaSortParm = sortOrder == "Categoria" ? "categoria_desc" : "Categoria";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var pois = from p in db.Poi
                             select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                pois = pois.Where(c => c.nome.Contains(searchString) || c.categoria.nome.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    pois = pois.OrderByDescending(c => c.nome);
                    break;
                case "Categoria":
                    pois = pois.OrderBy(c => c.categoria.nome);
                    break;
                case "categoria_desc":
                    pois = pois.OrderByDescending(c => c.categoria.nome);
                    break;
                default:
                    pois = pois.OrderBy(c => c.nome);
                    break;

            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            pois = pois.Include(p => p.categoria).Include(e => e.elementos);

            return View(pois.ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create([Bind(Include = "PoiID,CategoriaID,nome,latitude,longitude,descricao,resumo")] Poi poi, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string[] allowedImageExtensions = {".gif", ".png", ".jpeg", ".jpg"};
                    string[] allowedVideoExtensions = {".mp4"};
                    String fileExtension = Path.GetExtension(upload.FileName);
                    if (upload != null && upload.ContentLength > 0)
                    {
                        for (int i = 0; i < allowedImageExtensions.Length; i++)
                        {
                            if (fileExtension == allowedImageExtensions[i])
                            {
                                poi.ImagemPath = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                                poi.FileType = FileType.Imagem;
                                upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/GaleriaPoi/Imagem/"), poi.ImagemPath));
                            }
                        }
                        Console.WriteLine(poi.ImagemPath.ToString());
                        db.Poi.Add(poi);
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
        public ActionResult EditPost(int? id, HttpPostedFileBase upload)
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
                    if (upload != null && upload.ContentLength > 0)
                    {
                        string[] allowedImageExtensions = { ".gif", ".png", ".jpeg", ".jpg" };

                        String fileExtension = Path.GetExtension(upload.FileName);
                        foreach (string t in allowedImageExtensions)
                        {
                            if (fileExtension == t)
                            {
                                string currentFilePath = poiToUpdate.ImagemPath;
                                FileInfo file = new FileInfo(Path.Combine(Server.MapPath("~/Content/Images/GaleriaPoi/Imagem"), currentFilePath));
                                file.Delete();

                                var FileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                                var FileTypes = FileType.Imagem;
                                poiToUpdate.ImagemPath = FileName;
                                poiToUpdate.FileType = FileTypes;
                               
                                upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/GaleriaPoi/Imagem"), FileName));
                            }
                        }
                    }
                    if (ModelState.IsValid)
                    {
                        db.Entry(poiToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
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
