namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Configurations
{
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.Drafts;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MeetupDraftConfiguration : EntityTypeConfiguration<MeetupDraft>
    {
        public override void Map(EntityTypeBuilder<MeetupDraft> modelBuilder)
        {
            modelBuilder.HasKey(x => new { x.CommunityId, x.Id });

            modelBuilder.Property(e => e.CommunityId)
                .HasMaxLength(36);

            modelBuilder.Property(e => e.Id)
                .HasMaxLength(36);

            modelBuilder.HasMany(x => x.Talks);

            modelBuilder.HasMany(x => x.Friends);
        }
    }
}
