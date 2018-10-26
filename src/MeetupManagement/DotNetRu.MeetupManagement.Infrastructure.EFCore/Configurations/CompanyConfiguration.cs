namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Configurations
{
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public override void Map(EntityTypeBuilder<Company> modelBuilder)
        {
            modelBuilder.HasKey(e => e.Id);

            modelBuilder.Property(e => e.Id)
                .HasMaxLength(36);
        }
    }
}
