using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Data.DTO.Response
{
    public class Report
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public string ReportFilePath { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Kvartal { get; set; }
        public string Mandat { get; set; }
    }
}
