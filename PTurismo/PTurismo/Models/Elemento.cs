using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTurismo.Models
{
    public class Elemento
    {
        public int ElementoID { get; set; }
        public int PoiID { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string imagem { get; set; }
        public virtual Poi poi { get; set; }
        public virtual ICollection<GaleriaElemento> galeriaElemento { get; set; }
    }
}