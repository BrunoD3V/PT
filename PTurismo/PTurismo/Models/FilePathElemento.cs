using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTurismo.Models
{
    public class FilePathElemento
    {
        public int FilePathElementoID { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public int GaleriaElementoID { get; set; }
        public virtual GaleriaElemento GaleriaElemento { get; set; }
    }
}