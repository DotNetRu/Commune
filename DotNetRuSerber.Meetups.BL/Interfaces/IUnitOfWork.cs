using System.Threading.Tasks;

namespace DotNetRuSerber.Meetups.BL.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}