namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class EntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> modelBuilder);
    }
}
