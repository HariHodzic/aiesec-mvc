using Aiesec.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Repository.IServices
{
    public interface IReportService:ICRUDRepository<Data.Model.BusinessModel.Report, Data.DTO.Response.Report, Data.DTO.Request.Report, Data.DTO.Search.Report, int>
    {
        
    }
}
