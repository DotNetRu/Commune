using System.Threading.Tasks;

namespace DotNetRuServer.Meetups.BL.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}