using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.DAL.Database;

namespace DotNetRuServer.Meetups.DAL.Providers
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