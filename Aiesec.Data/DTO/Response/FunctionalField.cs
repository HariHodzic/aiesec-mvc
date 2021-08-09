using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Data.DTO.Response
{
    public class FunctionalField
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 5)]
        public string Name { get; set; }
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]

        public string Abbreviation { get; set; }
        [StringLength(200, ErrorMessage = "{0} length can't be more than {1}")]

        public string Description { get; set; }
    }
}
