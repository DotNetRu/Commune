namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Configurations
{
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.Drafts;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class VenueDraftConfiguration : EntityTypeConfiguration<VenueDraft>
    {
        public override void Map(EntityTypeBuilder<VenueDraft> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);

            modelBuilder.Property(e => e.Id)
                .HasMaxLength(36);
        }
    }
}
