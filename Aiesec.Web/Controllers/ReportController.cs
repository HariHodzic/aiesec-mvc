using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aiesec.Data.Context;
using Aiesec.Repository.IServices;
using AutoMapper;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Aiesec.Repository.Services;

namespace Aiesec.Web.Controllers
{
    public class ReportController : BaseController<Data.Model.BusinessModel.Report, Data.DTO.Request.Report, Data.DTO.Response.Report, Data.DTO.Search.Report, int>
    {

        private readonly IWebHostEnvironment _env;
        private readonly IFileService _fileService;
        public ReportController(IWebHostEnvironment env,IReportService service, AiesecDbContext context, IMapper mapper, IFileService fileService) : base(service, context, mapper)
        {
            _fileService = fileService;
            _env = env;
        }
        public override async Task<IActionResult> Insert([FromForm] Data.DTO.Request.Report model)
        {
            if (ModelState.IsValid)
            {
                var fileId=await _fileService.SaveFile(model.File);
                var insertModel = _mapper.Map<Data.Model.BusinessModel.Report>(model);
                insertModel.FileModelId = fileId;
                await _context.Reports.AddAsync(insertModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Create");
        }
        [HttpGet]
        public override async Task<IActionResult> Remove(int id)
        {
            int fileId = _context.Reports.FirstOrDefault(x => x.Id == id).FileModelId;
            await _fileService.DeleteFile(fileId);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DownloadFileAsync(int id)
        {
            var fileId = _context.Reports.FirstOrDefault(x => x.Id == id).FileModelId;
            var file =await _fileService.GetFile(fileId);
            return File(file.Data, file.FileType, file.Name + file.Extension);
        }   
    }
}
