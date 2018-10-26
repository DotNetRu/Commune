namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Configurations
{
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class FriendConfiguration : EntityTypeConfiguration<Friend>
    {
        public override void Map(EntityTypeBuilder<Friend> modelBuilder)
        {
            modelBuilder.HasKey(e => e.Id);

            modelBuilder.Property(e => e.Name)
                .IsRequired();

            modelBuilder.Property(e => e.Id)
                .HasMaxLength(36);
        }
    }
}
