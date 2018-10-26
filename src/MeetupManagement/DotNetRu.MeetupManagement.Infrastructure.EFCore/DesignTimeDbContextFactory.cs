namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.GetFullPath(@"..\..\ServiceHost\appsettings.Development.json"))
                .AddEnvironmentVariables();

            var configurationRoot = configurationBuilder.Build();
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseNpgsql(configurationRoot.GetConnectionString("DefaultConnection"));

            return new DataContext(builder.Options);
        }
    }
}
