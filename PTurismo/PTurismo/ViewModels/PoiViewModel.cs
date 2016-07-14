using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PTurismo.Models;

namespace PTurismo.ViewModels
{
    public class PoiViewModel
    {
        public Poi Poi { get; set; }
        public Elemento ElementoSelecionado { get; set; }
        public IEnumerable<Elemento> Elementos { get; set; }

    }
}