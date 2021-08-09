using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Data.DTO.Request
{
    public class FileModel
    {
        public byte[] Data { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public string UploadedBy { get; set; }
        public FileModel(IFormFile file)
        {
            Name = Path.GetFileNameWithoutExtension(file.FileName);
            FileType = file.ContentType;
            Extension = Path.GetExtension(file.FileName);
        }
    }
}
