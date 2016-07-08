using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTurismo.Models
{
    public class GaleriaElemento
    {
        public int GaleriaElementoID { get; set; }
        public int ElementoID { get; set; }
        public string media { get; set; }
        public string legenda { get; set; }
        public string tipoMedia { get; set; }
        public Elemento Elemento { get; set; }
    }
}