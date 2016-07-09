using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTurismo.Models
{
    public class GaleriaPoi
    {
        public virtual int GaleriaPoiID { get; set; }
        public virtual int PoiID { get; set; }
        public virtual string media { get; set; }
        public virtual string legenda { get; set; }
        public virtual string tipoMedia { get; set; }
        public virtual Poi Poi { get; set; }
    }
}