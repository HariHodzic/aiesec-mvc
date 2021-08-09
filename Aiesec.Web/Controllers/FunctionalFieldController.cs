using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiesec.Repository.IServices;
using AutoMapper;
using Aiesec.Data.Context;

namespace Aiesec.Web.Controllers
{
    public class FunctionalFieldController : BaseController<Data.Model.BusinessModel.FunctionalField,Data.DTO.Request.FunctionalField,Data.DTO.Response.FunctionalField,Data.DTO.Search.FunctionalField,int>
    {
        public FunctionalFieldController(Data.Context.AiesecDbContext context, IFunctionalFieldService service, IMapper mapper):base(service,context,mapper)
        {
        }
        
    }
}
