using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTurismo.Models
{
    public class Poi
    {
        public int PoiID { get; set; }
        public int CategoriaID { get; set; }
        public string nome { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string descricao { get; set; }
        public string imagem { get; set; }
        public string resumo { get; set; }
        public virtual Categoria categoria { get; set; }
        public virtual ICollection<Elemento> elementos { get; set; }
        public virtual ICollection<GaleriaPoi> galeriaPoi { get; set; }
    }
}