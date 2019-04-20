using FluentMigrator;

namespace DotNetRuServer.Migrations
{
    [Migration(1)]
    public class AddSpeakerTable : Migration
    {
        public override void Up()
        {
            var table = Create.Table("Speakers");
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
                .WithColumn("CompanyName")
                .AsString(100)
                .Nullable();
            table
                .WithColumn("CompanyUrl")
                .AsString(100)
                .Nullable();
            table
                .WithColumn("Description")
                .AsString(3000)
                .Nullable();
            table
                .WithColumn("BlogUrl")
                .AsString(300)
                .Nullable();
            table
                .WithColumn("ContactsUrl")
                .AsString(300)
                .Nullable();
            table
                .WithColumn("TwitterUrl")
                .AsString(300)
                .Nullable();
            table
                .WithColumn("HabrUrl")
                .AsString(300)
                .Nullable();
            table
                .WithColumn("GithubUrl")
                .AsString(300)
                .Nullable();
            table
                .WithColumn("AvatarUrl")
                .AsString(300)
                .Nullable();
            table
                .WithColumn("AvatarSmallUrl")
                .AsString(300)
                .Nullable();
        }

        public override void Down()
        {
            Delete.Table("Speakers");
        }
    }
}