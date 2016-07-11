using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PTurismo.DAL;

namespace PTurismo.ViewModels
{
    public class MapaViewModel
    {
        private PastoralContext db = new PastoralContext();
        public IEnumerable<PTurismo.Models.Poi> Pois { get; private set; }
        public IEnumerable<PTurismo.Models.Categoria> Categorias { get; private set; }
        public IEnumerable<PTurismo.Models.Elemento> Elementos { get; private set; }
        public IEnumerable<PTurismo.Models.GaleriaElemento> GaleriaElementos { get; private set; }
        public IEnumerable<PTurismo.Models.GaleriaPoi> GaleriaPois { get; private set; }
        public IEnumerable<PTurismo.Models.FilePathPoi> FilePathPois { get; private set; }
        public IEnumerable<PTurismo.Models.FilePathElemento> FilePathElementos { get; private set; }
        public MapaViewModel()
        {
            Pois = from p in db.Poi
                       select p;
            Categorias = from c in db.Categoria
                select c;
            Elementos = from e in db.Elemento
                select e;
            GaleriaElementos = from ge in db.GaleriaElemento
                select ge;
            GaleriaPois = from gp in db.GaleriaPoi
                select gp;
            FilePathPois = from fpp in db.FilePaths
                select fpp;
        }
    }
}