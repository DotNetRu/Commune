using System.Threading.Tasks;

namespace DevActivator.Meetups.BL.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}