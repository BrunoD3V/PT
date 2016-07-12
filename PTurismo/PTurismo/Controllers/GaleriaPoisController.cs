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
using System.Data.Entity.Infrastructure;

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
                galeriasPois = galeriasPois.Where(g => g.Poi.nome.Contains(searchString));
            }

            switch (sortOrder)
            {
                
                case "Legenda":
                    galeriasPois = galeriasPois.OrderBy(c => c.Legenda);
                    break;
                
                default:
                    galeriasPois = galeriasPois.OrderBy(g => g.Legenda);
                    break;

            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            galeriasPois = galeriasPois.Include(g => g.Poi)
                .Include(f => f.FilePaths);

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
        public ActionResult Create([Bind(Include = "GaleriaPoiID,PoiID,Legenda")] GaleriaPoi galeriaPoi, HttpPostedFileBase upload)
        {
            try
            {
                String fileExtension = Path.GetExtension(upload.FileName);
            
            if (ModelState.IsValid)
            {
                string[] allowedImageExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                string[] allowedVideoExtensions = { ".mp4"};
               
                if (upload != null && upload.ContentLength > 0)
                {
                    for (int i = 0; i < allowedImageExtensions.Length; i++)
                    {
                        if (fileExtension == allowedImageExtensions[i])
                        {
                            var file = new FilePathPoi
                            {
                                FileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName),
                                FileType = FileType.Imagem
                            };
                            galeriaPoi.FilePaths = new List<FilePathPoi>();
                            galeriaPoi.FilePaths.Add(file);
                            upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images"), file.FileName));
                        }
                    }
                    for (int i = 0; i < allowedVideoExtensions.Length; i++)
                    {
                        if (fileExtension == allowedVideoExtensions[i])
                        {
                            var file = new FilePathPoi
                            {
                                FileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName),
                                FileType = FileType.Video
                            };
                            galeriaPoi.FilePaths = new List<FilePathPoi>();
                            galeriaPoi.FilePaths.Add(file);
                            upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Videos"), file.FileName));
                        }
                    }
                    db.GaleriaPoi.Add(galeriaPoi);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                            
            }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Tem que introduzir um ficheiro");
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
            GaleriaPoi galeriaPoi = db.GaleriaPoi
                .Include(i => i.FilePaths)
                .Where(i => i.GaleriaPoiID == id)
                .Single();

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
        public ActionResult Edit([Bind(Include = "GaleriaPoiID,PoiID,Legenda")] GaleriaPoi galeriaPoi, HttpPostedFileBase upload)
        {
            var galeriaPoiToUpdate = db.GaleriaPoi.Find(galeriaPoi.GaleriaPoiID);
            if (ModelState.IsValid)
            {
                if (TryUpdateModel(galeriaPoiToUpdate, "", new string[] { "GaleriaPoiID", "PoiID", "Legenda" }))
                {
                    try
                    {
                        if (upload != null && upload.ContentLength > 0)
                        {
                            //Apaga a lista dos ficheiros associados e o ficheiro local associado
                            if (galeriaPoiToUpdate.FilePaths.Any(f => f.FileType == FileType.Imagem))
                            {
                                string currentFilePath = galeriaPoiToUpdate.FilePaths.First().FileName;
                                db.FilePaths.Remove(galeriaPoiToUpdate.FilePaths.First(f => f.FileType == FileType.Imagem));
                                FileInfo file = new FileInfo(Path.Combine(Server.MapPath("~/Content/Images"), currentFilePath));
                                file.Delete();
                            }
                            else
                            {
                                string currentFilePath = galeriaPoiToUpdate.FilePaths.First().FileName;
                                db.FilePaths.Remove(galeriaPoiToUpdate.FilePaths.First(f => f.FileType == FileType.Video));
                                FileInfo file = new FileInfo(Path.Combine(Server.MapPath("~/Content/Videos"), currentFilePath));
                                file.Delete();
                            }
                            //Verifica se o novo ficheiro é de uma extenção válida e adiciona-o à lista de ficheiros e insere localmente na pasta correspondente
                            string[] allowedImageExtensions = { ".png", ".jpeg", ".jpg" };
                            string[] allowedVideoExtensions = { ".gif" };
                            String fileExtension = Path.GetExtension(upload.FileName);
                            for (int i = 0; i < allowedImageExtensions.Length; i++)
                            {
                                if (fileExtension == allowedImageExtensions[i])
                                {
                                    var file = new FilePathPoi
                                    {
                                        FileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName),
                                        FileType = FileType.Imagem
                                    };
                                    galeriaPoiToUpdate.FilePaths.Add(file);
                                    upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images"), file.FileName));
                                }
                            }
                            for (int i = 0; i < allowedVideoExtensions.Length; i++)
                            {
                                if (fileExtension == allowedVideoExtensions[i])
                                {
                                    var file = new FilePathPoi
                                    {
                                        FileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName),
                                        FileType = FileType.Video
                                    };
                                    galeriaPoiToUpdate.FilePaths = new List<FilePathPoi>();
                                    galeriaPoiToUpdate.FilePaths.Add(file);
                                    upload.SaveAs(Path.Combine(Server.MapPath("~/Content/Videos"), file.FileName));
                                }
                            }
                        }
                        db.Entry(galeriaPoiToUpdate).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    catch (RetryLimitExceededException /* dex */)
                    {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
            }
            ViewBag.PoiID = new SelectList(db.Poi, "PoiID", "nome", galeriaPoiToUpdate.PoiID);
            return View(galeriaPoiToUpdate);
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
            string currentFilePath = galeriaPoi.FilePaths.First().FileName;
            FileInfo file = new FileInfo(Path.Combine(Server.MapPath("~/Content/Images"), currentFilePath));
            file.Delete();
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
