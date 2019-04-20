using FluentMigrator;

namespace DotNetRuServer.Migrations
{
    [Migration(11)]
    public class RemoveNotNullFriendLogo : Migration
    {
        public override void Up()
        {
            Alter.Table("Friends").AlterColumn("LogoUrl").AsString().Nullable();
            Alter.Table("Friends").AlterColumn("SmallLogoUrl").AsString().Nullable();
        }

        public override void Down()
        {
            Update.Table("Friends").Set(new { LogoUrl = string.Empty }).Where(new { LogoUrl = (string) null });
            Update.Table("Friends").Set(new { SmallLogoUrl = string.Empty }).Where(new { LogoUrl = (string) null });
            
            Alter.Table("Friends").AlterColumn("LogoUrl").AsString().NotNullable();
            Alter.Table("Friends").AlterColumn("SmallLogoUrl").AsString().NotNullable();
        }
    }
}