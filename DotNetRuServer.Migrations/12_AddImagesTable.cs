using FluentMigrator;

namespace DotNetRuServer.Migrations
{
    [Migration(12)]
    public sealed class AddImagesTable : Migration
    {
        public override void Up()
        {
            Create.Table("Images")

                .WithColumn("Id")
                    .AsInt32()
                    .NotNullable()
                    .Identity()
                    .PrimaryKey()

                .WithColumn("ExternalUrl")
                    .AsString(300)
                    .NotNullable()
                    .Unique()

                .WithColumn("MimeType")
                    .AsString(50)
                    .NotNullable()

                .WithColumn("IsSmall")
                    .AsBoolean()
                    .NotNullable()

                .WithColumn("Width")
                    .AsInt32()
                    .NotNullable()

                .WithColumn("Height")
                    .AsInt32()
                    .NotNullable()

                .WithColumn("Data")
                    .AsBinary(int.MaxValue)
                    .NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Images");
        }
    }
}