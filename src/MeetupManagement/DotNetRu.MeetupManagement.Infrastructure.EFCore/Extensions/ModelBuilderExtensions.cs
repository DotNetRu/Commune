namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Extensions
{
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Configurations;
    using Microsoft.EntityFrameworkCore;

    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TConfiguration, TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class
            where TConfiguration : EntityTypeConfiguration<TEntity>, new()
        {
            new TConfiguration().Map(modelBuilder.Entity<TEntity>());
        }
    }
}
