using Aiesec.Data.Context;
using Aiesec.Data.DTO.Request;
using Aiesec.Repository.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Repository.Services
{
    public class FileService : IFileService
    {
        public readonly AiesecDbContext _context;
        public readonly IMapper _mapper;
        public FileService(AiesecDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> SaveFile(IFormFile file)
        {
            FileModel fileModel = new FileModel(file);
            using (var dataStream = new MemoryStream())
            {
                await file.CopyToAsync(dataStream);
                fileModel.Data = dataStream.ToArray();
            }
            var fileEntity = _mapper.Map<Data.Model.BusinessModel.FileModel>(fileModel);
            _context.Files.Add(fileEntity);
            _context.SaveChanges();
            return fileEntity.Id;
        }
        public async Task<Data.DTO.Response.FileModel> GetFile(int id)
        {
            var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == id);
            if (file == null) return null;
            return _mapper.Map<Data.DTO.Response.FileModel>(file);
        }
        public async Task DeleteFile(int id)
        {
            var file = await _context.Files.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
        }

    }
}
