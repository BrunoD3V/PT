using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PTurismo.Models
{
    public class FilePathElemento
    {
        public int FilePathElementoID { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public int GaleriaElementoID { get; set; }
        [JsonIgnore]
        public virtual GaleriaElemento GaleriaElemento { get; set; }
    }
}