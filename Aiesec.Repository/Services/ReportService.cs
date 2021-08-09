using System;
using System.Threading.Tasks;
using Aiesec.Data.Context;
using AutoMapper;
using Aiesec.Repository.Repository;
using Aiesec.Repository.IServices;


namespace Aiesec.Repository
{
    public class ReportService:ICRUDRepository<Data.Model.BusinessModel.Report, Data.DTO.Response.Report, Data.DTO.Request.Report, Data.DTO.Search.Report, int>, IReportService
    {
        private readonly AiesecDbContext _context;
        private readonly IMapper _mapper;
        public ReportService(AiesecDbContext context,IMapper mapper):base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //public async Task<Data.DTO.Response.FileModel> GetFileFromReportById(int reportId) { 
        //    _context.
        //}
    }   
}
