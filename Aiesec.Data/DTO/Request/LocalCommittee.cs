using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiesec.Data.Validators;

namespace Aiesec.Data.DTO.Request
{
    public class LocalCommittee
    {
        [DataType(DataType.Date)]
        [CustomDate]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EstablishmentDate { get; set; }
        [Required]
        [NotNull]
        public int CityID { get; set; }
    }
}
        