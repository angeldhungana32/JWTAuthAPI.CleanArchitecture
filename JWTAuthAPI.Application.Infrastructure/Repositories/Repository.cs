using JWTAuthAPI.Application.Core.Interfaces;
using JWTAuthAPI.Application.Infrastructure.Data;
using JWTAuthAPI.Application.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthAPI.Application.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<T> EvaluateSpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>
                .GetQuery(_dbContext.Set<T>().AsQueryable(), specification);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(new object[] { id });
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(ISpecification<T> specification)
        {
            return await EvaluateSpecification(specification).ToListAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
