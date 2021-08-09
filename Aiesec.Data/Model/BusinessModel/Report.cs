using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Data.Model.BusinessModel
{
    public class Report:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Kvartal { get; set; }
        public string Mandat { get; set; }
        [Required]
        [ForeignKey(nameof(FileModel))]
        public int FileModelId { get; set; }

        public virtual FileModel FileModel { get; set; }
        
    }
}
