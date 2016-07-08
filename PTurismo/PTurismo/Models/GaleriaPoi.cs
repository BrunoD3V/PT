using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTurismo.Models
{
    public class GaleriaPoi
    {
        public int GaleriaPoiID { get; set; }
        public int PoiID { get; set; }
        public string media { get; set; }
        public string legenda { get; set; }
        public string tipoMedia { get; set; }
        public Poi Poi { get; set; }
    }
}