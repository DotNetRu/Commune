namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Extensions
{
    using Microsoft.EntityFrameworkCore;

    public static class DbContextExtensions
    {
        public static void Migrate(this DbContext context)
        {
            context.Database.Migrate();
        }
    }
}
