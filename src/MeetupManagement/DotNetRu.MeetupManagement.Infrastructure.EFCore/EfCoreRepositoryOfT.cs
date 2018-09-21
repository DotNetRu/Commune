using System;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable SA1649 // File name must match first type name
    internal class EfCoreRepository<TEntity, TKey>
#pragma warning restore SA1649 // File name must match first type name
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
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

#pragma warning disable CA1801 // Review unused parameters
        public void Delete(TKey key)
#pragma warning restore CA1801 // Review unused parameters
        {
            // var entity = GetEntity(id);
            // _dbContext.Set<TEntity>().Remove(entity);
            // await _dbContext.SaveChanges();
        }
    }
}
