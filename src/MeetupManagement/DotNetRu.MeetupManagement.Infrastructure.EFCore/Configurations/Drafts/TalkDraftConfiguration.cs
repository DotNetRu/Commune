namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Configurations
{
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.Drafts;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TalkDraftConfiguration : EntityTypeConfiguration<TalkDraft>
    {
        public override void Map(EntityTypeBuilder<TalkDraft> modelBuilder)
        {
            modelBuilder.HasKey(x => new { x.CommunityId, x.Id });

            modelBuilder.Property(e => e.CommunityId)
                .HasMaxLength(36);

            modelBuilder.Property(e => e.Id)
                .HasMaxLength(36);

            modelBuilder
                .HasMany(x => x.Speakers);

            modelBuilder
                .HasMany(x => x.Rehearsals);
        }
    }
}
