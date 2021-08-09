using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aiesec.Repository.IRepository
{
    public interface IReadRepository<TEntity, TResponse, TRequest, TSearch, TKey>
    {
        Task<IEnumerable<TResponse>> GetAllAsync();
        Task<TResponse> GetByIdAsyncResponse(TKey key);
        Task<TRequest> GetByIdAsyncRequest(TKey key);
        Task<IEnumerable<TResponse>> GetByParametersAsync(TSearch search);
    }
}
