using System;
using System.Collections.Generic;
using Aiesec.Repository.Repository;
using Aiesec.Repository.IRepository;

namespace Aiesec.Repository
{
    public interface IOfficeService : IRepository.ICRUDRepository<Data.Model.BusinessModel.Office, Data.DTO.Response.Office, Data.DTO.Request.Office, Data.DTO.Search.Office, int>
    {
    }
}
