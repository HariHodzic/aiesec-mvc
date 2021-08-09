using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Repository.IServices
{
    public interface IFileService
    {
        Task<int> SaveFile(IFormFile file);
        Task<Data.DTO.Response.FileModel> GetFile(int id);
        Task DeleteFile(int id);

    }
}
