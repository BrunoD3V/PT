using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PTurismo.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string nome { get; set; }
        public string genero { get; set; }
        public virtual ICollection<Poi> Poi { get; set; }
        //TODO
    }
}
