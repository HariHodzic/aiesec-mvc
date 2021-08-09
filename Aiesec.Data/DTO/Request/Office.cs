using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiesec.Data.Validators;

namespace Aiesec.Data.DTO.Request
{
    public class Office
    {
        [Required]
        [StringLength(40, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 5)]
        public string Address { get; set; }
        public int Capacity { get; set; }
        [DataType(DataType.Date)]
        [CustomDate]
        public DateTime EstablishmentDate { get; set; }
        [Required]
        [NotNull]
        public int CityID { get; set; } 
        public int? LocalCommitteeID { get; set; }
    }
}
