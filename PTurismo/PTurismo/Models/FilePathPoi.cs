using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PTurismo.Models
{
    public class FilePathPoi
    {
        public int FilePathPoiID { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public string Descricao { get; set; }
        public FileType FileType { get; set; }
        public int GaleriaPoiID { get; set; }
        [JsonIgnore]
        public virtual GaleriaPoi GaleriaPoi { get; set; }
    }
}