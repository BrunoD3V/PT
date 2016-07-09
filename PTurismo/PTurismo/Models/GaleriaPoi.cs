using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTurismo.Models
{
    public class GaleriaPoi
    {
        public int GaleriaPoiID { get; set; }
        public string Legenda { get; set; }
        public int PoiID { get; set; }
        public virtual Poi Poi { get; set; }
        public virtual ICollection<FilePathPoi> FilePaths { get; set; }
    }
}