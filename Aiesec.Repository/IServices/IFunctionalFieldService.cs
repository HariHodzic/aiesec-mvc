using Aiesec.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Repository.IServices
{
    public interface IFunctionalFieldService: ICRUDRepository<Data.Model.BusinessModel.FunctionalField,Data.DTO.Response.FunctionalField, Data.DTO.Request.FunctionalField,Data.DTO.Search.FunctionalField,int>
    {

    }
}
