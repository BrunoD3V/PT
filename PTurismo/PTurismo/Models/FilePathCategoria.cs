using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PTurismo.Models
{
    public class FilePathCategoria
    {
        [ForeignKey("Categoria")]
        public int FilePathCategoriaID { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public string Descricao { get; set; }
        public FileType FileType { get; set; }
        [JsonIgnore]
        public virtual Categoria Categoria { get; set; }
    }
}