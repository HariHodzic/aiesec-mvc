using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Data.DTO.Response
{
    public class Office
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        [DataType(DataType.Date)]
        public DateTime EstablishmentDate { get; set; }
        public bool Active { get; set; }
        public Data.Model.BusinessModel.City City { get; set; }
        public int CityId { get; set; }
        public Data.Model.BusinessModel.LocalCommittee LocalCommittee { get; set; }
        public int LocalCommitteeId { get; set; }
    }
}
