using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Data.Model.BusinessModel
{
    public class Office:BaseEntity<int>
    {
        [MaxLength(40)]
        public string Address { get; set; }
        public int Capacity { get; set; }
        [DataType(DataType.Date)]
        public DateTime EstablishmentDate { get; set; }
        [Required]
        public int CityID { get; set; }
        [ForeignKey("CityID")]
        public City City { get; set; }
        public int? LocalCommitteeId { get; set; }
        [ForeignKey(nameof(LocalCommitteeId))]
        public LocalCommittee LocalCommittee{ get; set; }
    }
}
