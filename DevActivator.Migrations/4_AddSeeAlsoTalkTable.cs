using FluentMigrator;

namespace DevActivator.Migrations
{
    [Migration(4)]
    public class AddSeeAlsoTalkTable : Migration
    {
        public override void Up()
        {
            var table = Create.Table("SeeAlsoTalks");

            table
                .WithColumn("ParentTalkId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("Talks", "Id");
            table
                .WithColumn("ChildTalkId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("Talks", "Id");

            Create.PrimaryKey("SeeAlsoTalksId")
                .OnTable("SeeAlsoTalks")
                .Columns("ParentTalkId", "ChildTalkId");
        }

        public override void Down()
        {
            Delete.Table("SeeAlsoTalks");
        }
    }
}