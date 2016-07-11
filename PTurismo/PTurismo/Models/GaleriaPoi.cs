using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PTurismo.Models
{
    public class GaleriaPoi
    {

        public virtual int GaleriaPoiID { get; set; }
        public virtual string Legenda { get; set; }
        public virtual int PoiID { get; set; }
        [JsonIgnore]
        public virtual Poi Poi { get; set; }
        [JsonIgnore]
        public virtual ICollection<FilePathPoi> FilePaths { get; set; }

    }
}