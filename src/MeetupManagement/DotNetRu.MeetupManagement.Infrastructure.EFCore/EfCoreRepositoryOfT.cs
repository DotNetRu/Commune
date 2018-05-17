using System;
using DotNetRu.MeetupManagement.Core.Shared;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    internal class EfCoreRepository<TEntity> : IRepository<TEntity>
    {
        public TEntity Get(long id)
        {
            // _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id)
            throw new NotImplementedException();
        }

        public long Create(TEntity draftTalk)
        {
            // _dbContext.Set<TEntity>().Add(entity);
            // _dbContext.SaveChanges();
            throw new NotImplementedException();
        }

        public void Update(TEntity draftTalk)
        {
            // _dbContext.Set<TEntity>().Update(entity);
            // _dbContext.SaveChanges();
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            // var entity = Get(id);
            // _dbContext.Set<TEntity>().Remove(entity);
            // await _dbContext.SaveChanges();
        }
    }
}
