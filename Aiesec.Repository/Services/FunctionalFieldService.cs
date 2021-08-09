using Aiesec.Data.Context;
using Aiesec.Repository.IServices;
using Aiesec.Repository.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Repository.Services
{
    public class FunctionalFieldService: ICRUDRepository<Data.Model.BusinessModel.FunctionalField, Data.DTO.Response.FunctionalField, Data.DTO.Request.FunctionalField, Data.DTO.Search.FunctionalField, int>, IFunctionalFieldService
    {
        public readonly AiesecDbContext _context;
        public readonly IMapper _mapper;

        public FunctionalFieldService(Data.Context.AiesecDbContext context, IMapper mapper):base(context,mapper)
        {
            _mapper = mapper;
            _context = context;

        }
    }
}
