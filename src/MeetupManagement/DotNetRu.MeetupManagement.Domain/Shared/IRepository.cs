
namespace DotNetRu.MeetupManagement.Domain.Shared
{
    public interface IRepository<TEntity, in TKey>
    {

        /// <exception cref="Domain.Contract.Exceptions.EntityNotFoundException" />
        TEntity Get(TKey id);
        /// <exception cref="Domain.Contract.Exceptions.EntityNotFoundException" />
        void Update(TEntity entity);
        /// <exception cref="Domain.Contract.Exceptions.EntityNotFoundException" />
        void Delete(TKey id);
    }
}
