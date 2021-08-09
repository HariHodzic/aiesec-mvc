using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiesec.Data.Context;
using AutoMapper;
using Aiesec.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace Aiesec.Repository
{
    public class OfficeService:ICRUDRepository<Data.Model.BusinessModel.Office, Data.DTO.Response.Office, Data.DTO.Request.Office, Data.DTO.Search.Office, int>, IOfficeService
    {
        public readonly AiesecDbContext _context;
        public readonly IMapper _mapper;

        public OfficeService(Aiesec.Data.Context.AiesecDbContext context,IMapper mapper):base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public override async Task<IEnumerable<Data.DTO.Response.Office>> GetAllAsync()
        {
            var result = await _context.Offices
                .Include(x=>x.City)
                .ToListAsync();

            return _mapper.Map<IList<Data.Model.BusinessModel.Office>,IEnumerable<Data.DTO.Response.Office>>(result);
        }
    }
}
