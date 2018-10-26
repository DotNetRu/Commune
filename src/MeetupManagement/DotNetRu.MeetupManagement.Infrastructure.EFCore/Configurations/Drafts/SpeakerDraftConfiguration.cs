namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Configurations
{
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.Drafts;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class SpeakerDraftConfiguration : EntityTypeConfiguration<SpeakerDraft>
    {
        public override void Map(EntityTypeBuilder<SpeakerDraft> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);

            modelBuilder.Property(e => e.Id)
                .HasMaxLength(36);
        }
    }
}
