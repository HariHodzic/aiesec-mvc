using Aiesec.Repository.IRepository;

namespace Aiesec.Repository.IServices
{
    public interface ILocalCommitteeService : 
        ICRUDRepository<Data.Model.BusinessModel.LocalCommittee, Data.DTO.Response.LocalCommittee, Data.DTO.Request.LocalCommittee, Data.DTO.Search.LocalCommittee, int>
    {
    }
}
