using Aiesec.Data.Context;
using Aiesec.Data.Model.BusinessModel;
using Aiesec.Repository.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Aiesec.Repository.Repository
{
    public class ICRUDRepository<TEntity, TResponse, TRequest, TSearch, TKey> :
         ReadRepository<TEntity, TResponse, TRequest, TSearch, TKey>,
         IRepository.ICRUDRepository<TEntity, TResponse, TRequest, TSearch, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly AiesecDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly DbSet<TEntity> _dbSet;

        public ICRUDRepository(AiesecDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task<TResponse> InsertAsync(TRequest entity)
        {
            var insertEntity = _mapper.Map<TEntity>(entity);
            try
            {
                await _dbSet.AddAsync(insertEntity);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<TResponse>(insertEntity);
            }
            catch (Exception)
            {
                throw; // TODO Dodati log
            }
          
        }

        public virtual async Task<TResponse> UpdateAsync(TKey key, TResponse entity)
        {
            var dbEntity = await _dbSet.FindAsync(key);
            try
            {
                _dbSet.Attach(dbEntity);
                _mapper.Map(entity, dbEntity);
                _dbContext.Update(dbEntity);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<TResponse>(dbEntity);
            }
            catch (Exception)
            {
                throw; // TODO Dodati log
            }
        }

        public virtual async Task<TResponse> DeleteAsync(TKey key)
        {
            TEntity dbEntity = await _dbContext.Set<TEntity>().FindAsync(key);
            try
            {
                _dbSet.Remove(dbEntity);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<TResponse>(dbEntity);
            }
            catch (Exception)
            {
                throw;  // TODO Dodati log
            }
        }
    }
}
