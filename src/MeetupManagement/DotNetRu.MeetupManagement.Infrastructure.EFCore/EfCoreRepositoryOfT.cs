using System;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    internal class EfCoreRepository<TEntity, TKey>
    {
        public TEntity Get(TKey key)
        {
            // _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id)
            throw new NotImplementedException();
        }

        public long Create(TEntity entity)
        {
            // _dbContext.Set<TEntity>().Add(entity);
            // _dbContext.SaveChanges();
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            // _dbContext.Set<TEntity>().Update(entity);
            // _dbContext.SaveChanges();
            throw new NotImplementedException();
        }

        public void Delete(TKey key)
        {
            // var entity = Get(id);
            // _dbContext.Set<TEntity>().Remove(entity);
            // await _dbContext.SaveChanges();
        }
    }
}
