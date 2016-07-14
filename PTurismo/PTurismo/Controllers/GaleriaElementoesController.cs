using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
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
            ViewBag.LegendaSortParm = String.IsNullOrEmpty(sortOrder) ? "legenda_desc" : "";
            

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
                galeriasElementos = galeriasElementos.Where(g => g.legenda.Contains(searchString) );
            }

            switch (sortOrder)
            {
               
               
                case "legenda_desc":
                    galeriasElementos = galeriasElementos.OrderByDescending(c => c.legenda);
                    break;
                default:
                    galeriasElementos = galeriasElementos.OrderBy(c => c.legenda);
                    break;
                 

            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            galeriasElementos = galeriasElementos.Include(g => g.Elemento);

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
        public ActionResult Create([Bind(Include = "GaleriaElementoID,ElementoID,legenda")] GaleriaElemento galeriaElemento, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string[] allowedImageExtensions = {".gif", ".png", ".jpeg", ".jpg"};
                    string[] allowedVideoExtensions = {".mp4", ".mp3", ".wav"};
                    string[] allowedAudioExtensions = { ".mp4", ".mp3", ".wav" };
                    String fileExtension = Path.GetExtension(upload.FileName);
                    if (upload != null && upload.ContentLength > 0)
                    {
                        for (int i = 0; i < allowedImageExtensions.Length; i++)
                        {
                            if (fileExtension == allowedImageExtensions[i])
                            {
                                var file = new FilePathElemento
                                {
                                    FileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName),
                                    FileType = FileType.Imagem
                                };
                                galeriaElemento.FilePathElementos = new List<FilePathElemento>();
                                galeriaElemento.FilePathElementos.Add(file);
                                upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/GaleriaElemento/"), file.FileName));
                            }
                        }
                        for (int i = 0; i < allowedVideoExtensions.Length; i++)
                        {
                            if (fileExtension == allowedVideoExtensions[i])
                            {
                                var file = new FilePathElemento()
                                {
                                    FileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName),
                                    FileType = FileType.Video
                                };
                                galeriaElemento.FilePathElementos = new List<FilePathElemento>();
                                galeriaElemento.FilePathElementos.Add(file);
                                upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Videos/GaleriaElemento/"), file.FileName));
                            }
                        }
                        for (int i = 0; i < allowedAudioExtensions.Length; i++)
                        {
                            if (fileExtension == allowedAudioExtensions[i])
                            {
                                var file = new FilePathElemento()
                                {
                                    FileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName),
                                    FileType = FileType.Audio
                                };
                                galeriaElemento.FilePathElementos = new List<FilePathElemento>();
                                galeriaElemento.FilePathElementos.Add(file);
                                upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Audio/GaleriaElemento/"), file.FileName));
                            }
                        }
                    }
                    db.GaleriaElemento.Add(galeriaElemento);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Tem que adicionar um ficheiro");
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
        public ActionResult Edit([Bind(Include = "GaleriaElementoID,ElementoID,legenda")] GaleriaElemento galeriaElemento, HttpPostedFileBase upload)
        {
            var galeriaElementoToUpdate = db.GaleriaElemento.Find(galeriaElemento.GaleriaElementoID);
            if (ModelState.IsValid)
            {
                if (TryUpdateModel(galeriaElementoToUpdate, "", new string[] { "GaleriaElementoID,ElementoID,legenda" }))
                {
                    try
                    {
                        if (upload != null && upload.ContentLength > 0)
                        {
                            //Apaga a lista dos ficheiros associados e o ficheiro local associado
                            if (galeriaElementoToUpdate.FilePathElementos.Any(f => f.FileType == FileType.Imagem))
                            {
                                string currentFilePath = galeriaElementoToUpdate.FilePathElementos.First().FileName;
                                db.FilePathsEl.Remove(
                                    galeriaElementoToUpdate.FilePathElementos.First(f => f.FileType == FileType.Imagem));
                                FileInfo file =
                                    new FileInfo(Path.Combine(Server.MapPath("~/Content/Images/GaleriaElemento/"), currentFilePath));
                                file.Delete();
                            }else if(galeriaElementoToUpdate.FilePathElementos.Any(f => f.FileType == FileType.Audio))
                            {
                                string currentFilePath = galeriaElementoToUpdate.FilePathElementos.First().FileName;
                                db.FilePathsEl.Remove(
                                    galeriaElementoToUpdate.FilePathElementos.First(f => f.FileType == FileType.Audio));
                                FileInfo file =
                                    new FileInfo(Path.Combine(Server.MapPath("~/Content/Images/GaleriaElemento/"), currentFilePath));
                                file.Delete();
                            }
                            else if(galeriaElementoToUpdate.FilePathElementos.Any(f => f.FileType == FileType.Video))
                            {
                                string currentFilePath = galeriaElementoToUpdate.FilePathElementos.First().FileName;
                                db.FilePathsEl.Remove(galeriaElementoToUpdate.FilePathElementos.First(f => f.FileType == FileType.Video));
                                FileInfo file =
                                    new FileInfo(Path.Combine(Server.MapPath("~/Content/Videos/GaleriaElemento/"), currentFilePath));
                                file.Delete();
                            }
                            //Verifica se o novo ficheiro é de uma extenção válida e adiciona-o à lista de ficheiros e insere localmente na pasta correspondente
                            string[] allowedImageExtensions = {".png", ".jpeg", ".jpg"};
                            string[] allowedVideoExtensions = {".gif"};
                            string[] allowedAudioExtensions = { ".mp4", ".mp3", ".wav" };
                            String fileExtension = Path.GetExtension(upload.FileName);
                            for (int i = 0; i < allowedImageExtensions.Length; i++)
                            {
                                if (fileExtension == allowedImageExtensions[i])
                                {
                                    var file = new FilePathElemento
                                    {
                                        FileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName),
                                        FileType = FileType.Imagem
                                    };
                                    galeriaElementoToUpdate.FilePathElementos.Add(file);
                                    upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/GaleriaElemento/"), file.FileName));
                                }
                            }
                            for (int i = 0; i < allowedVideoExtensions.Length; i++)
                            {
                                if (fileExtension == allowedVideoExtensions[i])
                                {
                                    var file = new FilePathElemento
                                    {
                                        FileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName),
                                        FileType = FileType.Video
                                    };
                                 
                                    galeriaElementoToUpdate.FilePathElementos.Add(file);
                                    upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Videos/GaleriaElemento"), file.FileName));
                                }
                            }
                            for (int i = 0; i < allowedAudioExtensions.Length; i++)
                            {
                                if (fileExtension == allowedAudioExtensions[i])
                                {
                                    var file = new FilePathElemento()
                                    {
                                        FileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName),
                                        FileType = FileType.Audio
                                    };
                                  
                                    galeriaElementoToUpdate.FilePathElementos.Add(file);
                                    upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Audio/GaleriaElemento/"), file.FileName));
                                }
                            }
                        }
                        db.Entry(galeriaElementoToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (RetryLimitExceededException /* dex */)
                    {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        ModelState.AddModelError("",
                            "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }

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
            if (galeriaElemento.FilePathElementos.Any(f => f.FileType == FileType.Imagem))
            {
                string currentFilePath = galeriaElemento.FilePathElementos.First().FileName;
                db.FilePathsEl.Remove(
                    galeriaElemento.FilePathElementos.First(f => f.FileType == FileType.Imagem));
                FileInfo file =
                    new FileInfo(Path.Combine(Server.MapPath("~/Content/Images/GaleriaElemento/"), currentFilePath));
                file.Delete();
            }
            else if (galeriaElemento.FilePathElementos.Any(f => f.FileType == FileType.Audio))
            {
                string currentFilePath = galeriaElemento.FilePathElementos.First().FileName;
                db.FilePathsEl.Remove(
                    galeriaElemento.FilePathElementos.First(f => f.FileType == FileType.Audio));
                FileInfo file =
                    new FileInfo(Path.Combine(Server.MapPath("~/Content/Images/GaleriaElemento/"), currentFilePath));
                file.Delete();
            }
            else if (galeriaElemento.FilePathElementos.Any(f => f.FileType == FileType.Video))
            {
                string currentFilePath = galeriaElemento.FilePathElementos.First().FileName;
                db.FilePathsEl.Remove(galeriaElemento.FilePathElementos.First(f => f.FileType == FileType.Video));
                FileInfo file =
                    new FileInfo(Path.Combine(Server.MapPath("~/Content/Videos/GaleriaElemento/"), currentFilePath));
                file.Delete();
            }
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