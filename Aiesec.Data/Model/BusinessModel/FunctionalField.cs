using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Data.Model.BusinessModel
{
    public class FunctionalField:BaseEntity<int>
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [MaxLength(5)]
        public string Abbreviation { get; set; }
        public string Description { get; set; }
    }
}
