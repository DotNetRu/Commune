using FluentMigrator;

namespace DevActivator.Migrations
{
    [Migration(9)]
    public class AddSession : Migration
    {
        public override void Up()
        {
            var table = Create.Table("Sessions");
            table
                .WithColumn("Id")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()
                .Identity();

            table
                .WithColumn("TalkId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("Talks", "Id");
            table
                .WithColumn("MeetupId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("Meetups", "Id");

            table
                .WithColumn("StartTime")
                .AsDateTime()
                .NotNullable();
            table
                .WithColumn("EndTime")
                .AsDateTime()
                .NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Sessions");
        }
    }
}