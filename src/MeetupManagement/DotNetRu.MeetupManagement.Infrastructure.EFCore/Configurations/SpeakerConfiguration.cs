namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Configurations
{
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class SpeakerConfiguration : EntityTypeConfiguration<Speaker>
    {
        public override void Map(EntityTypeBuilder<Speaker> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);

            modelBuilder.Property(e => e.Id)
                .HasMaxLength(36);
        }
    }
}
