using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiesec.Repository.Repository;
using Aiesec.Repository.IServices;
using AutoMapper;
using Aiesec.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Aiesec.Repository.Services
{
    public class LocalCommitteeService: ICRUDRepository<Data.Model.BusinessModel.LocalCommittee, Data.DTO.Response.LocalCommittee, Data.DTO.Request.LocalCommittee, Data.DTO.Search.LocalCommittee, int>, ILocalCommitteeService
    {
        public readonly AiesecDbContext _context;
        public readonly IMapper _mapper;
        public LocalCommitteeService(Aiesec.Data.Context.AiesecDbContext context, IMapper mapper):base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public override async Task<IEnumerable<Data.DTO.Response.LocalCommittee>> GetAllAsync()
        {
            var result = await _context.LocalCommittee
                .Include(x=>x.City)
                .ToListAsync();

            return _mapper.Map<IList<Data.Model.BusinessModel.LocalCommittee>, IEnumerable<Data.DTO.Response.LocalCommittee>>(result);
        }
        public override async Task<Data.DTO.Response.LocalCommittee> GetByIdAsyncResponse(int key)
        {
            var result = await _context.LocalCommittee.Include(x=>x.City).Include(x=>x.Offices)
                .FirstOrDefaultAsync(x => x.Id == key);
            if (result == null)
            {
                throw new Exception("Entity not found.");
            }

            return _mapper.Map<Data.DTO.Response.LocalCommittee>(result);
        }
        public override async Task<Data.DTO.Response.LocalCommittee> DeleteAsync(int key)
        {
            var dbEntity = await  _context.LocalCommittee.Include(x=>x.Offices).FirstOrDefaultAsync(x=>x.Id==key);
            try
            {
                _context.Remove(dbEntity);
                await _context.SaveChangesAsync();
                return _mapper.Map<Data.DTO.Response.LocalCommittee>(dbEntity);
            }
            catch (Exception)
            {
                throw;  // TODO Dodati log
            }
        }
    }
}
 