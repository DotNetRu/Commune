namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable SA1649 // File name must match first type name
    internal class EfCoreRepository<TEntity, TKey>
        where TEntity : class
#pragma warning restore SA1649 // File name must match first type name
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> dbSet;

        public EfCoreRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual TEntity GetByID(params object[] keyValues) => this.dbSet.Find(keyValues);

        public virtual TEntity Get(TKey key)
        {
            // _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id)
            throw new NotImplementedException();
        }

        public virtual long Create(TEntity entity)
        {
            entity = entity ?? throw new ArgumentNullException(nameof(entity));
            // _dbContext.Set<TEntity>().Add(entity);
            // _dbContext.SaveChanges();
            throw new NotImplementedException();
        }

        public virtual void Update(TEntity entity)
        {
            entity = entity ?? throw new ArgumentNullException(nameof(entity));
            // _dbContext.Set<TEntity>().Update(entity);
            // _dbContext.SaveChanges();
            throw new NotImplementedException();
        }

#pragma warning disable CA1801 // Review unused parameters
        public virtual void Delete(TKey key)
#pragma warning restore CA1801 // Review unused parameters
        {
            //this.SetState(entity, EntityState.Deleted);
            // var entity = GetEntity(id);
            // _dbContext.Set<TEntity>().Remove(entity);
            // await _dbContext.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync() => this.context.SaveChangesAsync();

        protected virtual void SetState(object entity, EntityState state)
        {
            entity = entity ?? throw new ArgumentNullException(nameof(entity));

            if (this.context.Entry(entity).State == EntityState.Detached)
            {
                this.context.Entry(entity).State = state;
            }
            else if (this.context.Entry(entity).State != state)
            {
                if (this.context.Entry(entity).State == EntityState.Added && state == EntityState.Modified)
                {
                    // leave state unchanged
                }
                else
                {
                    this.context.Entry(entity).State = state;
                }
            }
        }
    }
}
