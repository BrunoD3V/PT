using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PTurismo.Models
{
    public class Poi
    {
        public virtual int PoiID { get; set; }
        public virtual int CategoriaID { get; set; }
        public virtual string nome { get; set; }
        public virtual double latitude { get; set; }
        public virtual double longitude { get; set; }
        public virtual string descricao { get; set; }
        public virtual FilePathPoi ImagemPath { get; set; }
        public virtual string resumo { get; set; }
        [JsonIgnore]
        public virtual Categoria categoria { get; set; }
        [JsonIgnore]
        public virtual ICollection<Elemento> elementos { get; set; }
        [JsonIgnore]
        public virtual ICollection<GaleriaPoi> galeriaPoi { get; set; }
    }
}
