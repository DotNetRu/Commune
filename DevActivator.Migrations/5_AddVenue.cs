using FluentMigrator;

namespace DevActivator.Migrations
{
    [Migration(5)]
    public class AddVenue : Migration
    {
        public override void Up()
        {
            var table = Create.Table("Venues");
            table
                .WithColumn("Id")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()
                .Identity();
            table
                .WithColumn("ExportId")
                .AsString(100)
                .NotNullable()
                .Unique();

            table
                .WithColumn("Name")
                .AsString(100)
                .NotNullable();
            table
                .WithColumn("City")
                .AsByte()
                .NotNullable();
            table
                .WithColumn("Address")
                .AsString(350)
                .NotNullable();
            table
                .WithColumn("MapUrl")
                .AsString(350);
        }

        public override void Down()
        {
            Delete.Table("Venues");
        }
    }
}