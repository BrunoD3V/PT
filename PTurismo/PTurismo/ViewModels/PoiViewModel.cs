using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PTurismo.Models;

namespace PTurismo.ViewModels
{
    public class PoiViewModel
    {
        public IEnumerable<Poi> Pois { get; private set; }

        public PoiViewModel(IEnumerable<Poi> pois )
        {
            Pois = pois;
        }

    }
}