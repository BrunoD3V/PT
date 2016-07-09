using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PTurismo.Models
{
    public class GaleriaElemento
    {
        public virtual int GaleriaElementoID { get; set; }
        public virtual int ElementoID { get; set; }
        public virtual string media { get; set; }
        public virtual string legenda { get; set; }
        public virtual string tipoMedia { get; set; }
        [JsonIgnore]
        public virtual Elemento Elemento { get; set; }
    }
}