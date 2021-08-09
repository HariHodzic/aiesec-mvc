using Aiesec.Data.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Data.DTO.Request
{
    public class Report
    {
        [Required(ErrorMessage ="Please select a file.")]
        [AllowedExtensions(new string[] { ".pdf" })]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile File { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 5)]
        public string Name { get; set; }
        [StringLength(40, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 5)]
        public string Description { get; set; }
        [Required]
        [RegularExpression(@"^([1-9]{1}[0-9]{3}[-]?)*([1-9]{1}[0-9]{1})$",ErrorMessage ="Mandate has to be in fotmat YYYY-YY")]
        public string Mandat { get; set; }
        [Range(1,4)]
        public int Kvartal { get; set; }
        public int FileModelId { get; set; }
    }
}
