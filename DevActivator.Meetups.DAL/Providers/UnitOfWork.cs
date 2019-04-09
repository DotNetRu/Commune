using System.Threading.Tasks;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.DAL.Database;

namespace DevActivator.Meetups.DAL.Providers
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DotNetRuServerContext _context;

        public UnitOfWork(DotNetRuServerContext context)
        {
            _context = context;
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}