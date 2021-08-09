using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Data.DTO.Search
{
    public class Office
    {
        public string Address { get; set; }
        public int Capacity { get; set; }
        public DateTime EstablishmentDate { get; set; }
        public int? Active { get; set; }
    }
}
