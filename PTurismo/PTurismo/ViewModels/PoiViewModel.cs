using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PTurismo.Models;

namespace PTurismo.ViewModels
{
    public class PoiViewModel
    {
        public IEnumerable<PTurismo.Models.Poi> Pois { get; private set; }

        public PoiViewModel(IEnumerable<PTurismo.Models.Poi> pois )
        {
            Pois = pois;
        }

    }
}