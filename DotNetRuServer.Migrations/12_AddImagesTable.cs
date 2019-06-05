using FluentMigrator;

namespace DotNetRuServer.Migrations
{
    [Migration(12)]
    public sealed class AddImagesSupport : Migration
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
                    .Nullable()

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

            Delete.Column("LogoUrl").FromTable("Friends");
            Delete.Column("SmallLogoUrl").FromTable("Friends");
            Delete.Column("AvatarUrl").FromTable("Speakers");
            Delete.Column("AvatarSmallUrl").FromTable("Speakers");

            Alter.Table("Speakers").AddColumn("AvatarId")
                .AsInt32()
                .Nullable()
                .ForeignKey("Images", "Id");

            Alter.Table("Speakers").AddColumn("AvatarSmallId")
                .AsInt32()
                .Nullable()
                .ForeignKey("Images", "Id");

            Alter.Table("Friends").AddColumn("LogoId")
                .AsInt32()
                .Nullable()
                .ForeignKey("Images", "Id");

            Alter.Table("Friends").AddColumn("SmallLogoId")
                .AsInt32()
                .Nullable()
                .ForeignKey("Images", "Id");
        }

        public override void Down()
        {
            Delete.Table("Images");

            Alter.Table("Friends").AddColumn("SmallLogoUrl").AsString(500).NotNullable();
            Alter.Table("Friends").AddColumn("LogoUrl").AsString(500).NotNullable();

            Alter.Table("Speakers")
                .AddColumn("AvatarUrl")
                .AsString(300)
                .Nullable();
            Alter.Table("Speakers")
                .AddColumn("AvatarSmallUrl")
                .AsString(300)
                .Nullable();
        }
    }
}