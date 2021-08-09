using System.Threading.Tasks;

namespace Aiesec.Repository.IRepository
{
    public interface ICRUDRepository<TEntity, TResponse, TRequest, TSearch, TKey> : IReadRepository<TEntity, TResponse, TRequest, TSearch, TKey>
    {
        Task<TResponse> InsertAsync(TRequest entity);
        Task<TResponse> UpdateAsync(TKey key, TResponse entity);
        Task<TResponse> DeleteAsync(TKey key);
    }
}
