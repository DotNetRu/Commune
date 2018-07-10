
namespace DotNetRu.MeetupManagement.Domain.Shared
{
    public interface IRepository<TEntity, in TKey>
    {
        TEntity Get(TKey id);
        void Update(TEntity entity);
        void Delete(TKey id);
    }
}
