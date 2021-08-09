using Aiesec.Data.Context;
using Aiesec.Data.Model.BusinessModel;
using Aiesec.Repository.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aiesec.Repository.Repository
{
    public class ReadRepository<TEntity, TResponse, TRequest, TSearch, TKey> :
        IReadRepository<TEntity, TResponse, TRequest, TSearch, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly AiesecDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly DbSet<TEntity> _dbSet;

        public ReadRepository(AiesecDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TResponse>> GetAllAsync()
        {
            var result = await _dbSet.ToListAsync();
            return _mapper.Map<IEnumerable<TResponse>>(result);
        }

        public virtual async Task<TResponse> GetByIdAsyncResponse(TKey key)
        {
            var result = await _dbSet.FindAsync(key);
            if (result == null)
            {
                throw new Exception("Entity not found.");
            }

            return _mapper.Map<TResponse>(result);
        }
        public async Task<TRequest> GetByIdAsyncRequest(TKey key)
        {
            var result = await _dbSet.FindAsync(key);
            if (result == null)
            {
                throw new Exception("Entity not found.");
            }

            return _mapper.Map<TRequest>(result);
        }

        public async Task<IEnumerable<TResponse>> GetByParametersAsync(TSearch search)
        {
            var result = await _dbSet.ToListAsync();
            return _mapper.Map<IEnumerable<TResponse>>(result);
        }
    }
}
