using FluentMigrator;

namespace DotNetRuServer.Migrations
{
    [Migration(10)]
    public class AddFriendAtMeetup : Migration
    {
        public override void Up()
        {
            var table = Create.Table("FriendAtMeetup");
            table
                .WithColumn("MeetupId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("Meetups", "Id");
            table
                .WithColumn("FriendId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("Friends", "Id");

            Create.PrimaryKey("FriendAtMeetupId")
                .OnTable("FriendAtMeetup")
                .Columns("MeetupId", "FriendId");
        }

        public override void Down()
        {
            Delete.Table("FriendAtMeetup");
        }
    }
}