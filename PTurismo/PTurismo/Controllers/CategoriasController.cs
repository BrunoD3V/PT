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
using System.IO;

namespace PTurismo.Controllers
{
    public class CategoriasController : Controller
    {
        private PastoralContext db = new PastoralContext();

        // GET: Categorias
        public ActionResult Index(string sortOrder,string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.GeneroSortParm = sortOrder== "Genero" ? "genero_desc" : "Genero";

            if(searchString!=null)
            {
                page = 1;
            }else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var categorias = from c in db.Categoria
                             select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                categorias = categorias.Where(c => c.nome.Contains(searchString) || c.genero.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    categorias = categorias.OrderByDescending(c => c.nome);
                    break;
                case "Genero":
                    categorias = categorias.OrderBy(c => c.genero);
                    break;
                case "genero_desc":
                    categorias = categorias.OrderByDescending(c => c.genero);
                    break;
                default:
                    categorias = categorias.OrderBy(c => c.nome);
                    break;
                   
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(categorias.ToPagedList(pageNumber, pageSize));
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nome,genero")] Categoria categoria, HttpPostedFileBase upload)
        {
            bool validName = false;
            try
            {
                foreach (var c in db.Categoria)
                {
                    if (upload.FileName.Equals(c.nome))
                        validName = true;
                }
                string nomeF = categoria.nome + Path.GetExtension(upload.FileName);
                if (nomeF.Equals(upload.FileName))
                {
                    validName = true;
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Tem que introduzir um ficheiro");
            }
            if (!validName)
                return View(categoria);
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
                                var FileName =upload.FileName + Path.GetExtension(upload.FileName);
                                var FileTypes = FileType.Imagem;
                                
                                categoria.FilePathCategoria = new FilePathCategoria();
                                categoria.FilePathCategoria.FileName = FileName;
                                categoria.FilePathCategoria.FileType = FileTypes;
                                upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/Icones"), FileName));
                            }
                        }

                        db.Categoria.Add(categoria);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Erro a guardar alterações. Tente outra vez, se o problem persistir contacte o administrador do sitema.");
            }
                
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoriaID,nome,genero")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categoria.Find(id);
            db.Categoria.Remove(categoria);
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
