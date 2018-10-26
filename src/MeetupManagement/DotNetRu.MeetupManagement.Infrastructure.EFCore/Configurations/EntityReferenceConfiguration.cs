namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Configurations
{
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class EntityReferenceConfiguration : EntityTypeConfiguration<EntityReference>
    {
        public override void Map(EntityTypeBuilder<EntityReference> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);

            modelBuilder.Property(e => e.Id)
                .HasMaxLength(36);

            modelBuilder.Property(e => e.Name)
                .IsRequired();
        }
    }
}
