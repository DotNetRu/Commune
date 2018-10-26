namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Configurations
{
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.Common;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public override void Map(EntityTypeBuilder<Person> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);

            modelBuilder.Property(e => e.Id)
                .HasMaxLength(36);
        }
    }
}
