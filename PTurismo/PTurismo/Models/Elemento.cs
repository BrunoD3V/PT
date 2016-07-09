using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PTurismo.Models
{
    public class Elemento
    {
        public virtual int ElementoID { get; set; }
        public virtual int PoiID { get; set; }
        public virtual string nome { get; set; }
        public virtual string descricao { get; set; }
        public virtual string imagem { get; set; }
        public virtual Poi poi { get; set; }
        [JsonIgnore]
        public virtual ICollection<GaleriaElemento> galeriaElemento { get; set; }
    }
}