namespace DotNetRu.MeetupManagement.Domain.Common
{
    public interface IRepository<TEntity, in TKey>
    {
        /// <exception cref="Domain.Contract.Exceptions.EntityNotFoundException" />
        TEntity GetEntity(TKey id);

        /// <exception cref="Domain.Contract.Exceptions.EntityNotFoundException" />
        void Update(TEntity entity);

        /// <exception cref="Domain.Contract.Exceptions.EntityNotFoundException" />
        void Delete(TKey id);
    }
}
