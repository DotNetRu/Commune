using FluentMigrator;

namespace DevActivator.Migrations
{
    [Migration(2)]
    public class AddTalkTable : Migration
    {
        public override void Up()
        {
            var table = Create.Table("Talks");
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
                .WithColumn("Title")
                .AsString(500)
                .NotNullable();
            table
                .WithColumn("Description")
                .AsString(5000);
            table
                .WithColumn("CodeUrl")
                .AsString(300)
                .Nullable();
            table
                .WithColumn("SlidesUrl")
                .AsString(300)
                .Nullable();
            table
                .WithColumn("VideoUrl")
                .AsString(300)
                .Nullable();
        }

        public override void Down()
        {
            Delete.Table("Talks");
        }
    }
}