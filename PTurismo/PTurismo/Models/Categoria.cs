using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PTurismo.Models
{
    public class Categoria
    {
        public virtual int CategoriaID { get; set; }
        public virtual string nome { get; set; }
        public virtual string genero { get; set; }
       
        
        public virtual FilePathCategoria FilePathCategoria { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Poi> Poi { get; set; }

        
    }
}
