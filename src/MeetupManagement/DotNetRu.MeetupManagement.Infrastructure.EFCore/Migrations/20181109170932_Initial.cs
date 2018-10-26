using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FriendDrafts",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendDrafts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VenueDrafts",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    MapUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueDrafts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerDrafts",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BlogsUrl = table.Column<string>(nullable: true),
                    ContactsUrl = table.Column<string>(nullable: true),
                    TwitterUrl = table.Column<string>(nullable: true),
                    GitHubUrl = table.Column<string>(nullable: true),
                    CompanyId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerDrafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakerDrafts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BlogsUrl = table.Column<string>(nullable: true),
                    ContactsUrl = table.Column<string>(nullable: true),
                    TwitterUrl = table.Column<string>(nullable: true),
                    GitHubUrl = table.Column<string>(nullable: true),
                    CompanyId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Speakers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeetupDrafts",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    VenueId = table.Column<string>(nullable: true),
                    CommunityId = table.Column<string>(maxLength: 36, nullable: false),
                    Id = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetupDrafts", x => new { x.CommunityId, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "TalkDrafts",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    CommunityId = table.Column<string>(maxLength: 36, nullable: false),
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    MeetupDraftCommunityId = table.Column<string>(nullable: true),
                    MeetupDraftId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalkDrafts", x => new { x.CommunityId, x.Id });
                    table.ForeignKey(
                        name: "FK_TalkDrafts_MeetupDrafts_MeetupDraftCommunityId_MeetupDraftId",
                        columns: x => new { x.MeetupDraftCommunityId, x.MeetupDraftId },
                        principalTable: "MeetupDrafts",
                        principalColumns: new[] { "CommunityId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityReferences",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IsDraft = table.Column<bool>(nullable: false),
                    MeetupDraftCommunityId = table.Column<string>(nullable: true),
                    MeetupDraftId = table.Column<string>(nullable: true),
                    TalkDraftCommunityId = table.Column<string>(nullable: true),
                    TalkDraftId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityReferences_MeetupDrafts_MeetupDraftCommunityId_Meetup~",
                        columns: x => new { x.MeetupDraftCommunityId, x.MeetupDraftId },
                        principalTable: "MeetupDrafts",
                        principalColumns: new[] { "CommunityId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityReferences_TalkDrafts_TalkDraftCommunityId_TalkDraftId",
                        columns: x => new { x.TalkDraftCommunityId, x.TalkDraftId },
                        principalTable: "TalkDrafts",
                        principalColumns: new[] { "CommunityId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TalkRehearsals",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    TalkDraftCommunityId = table.Column<string>(nullable: true),
                    TalkDraftId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalkRehearsals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TalkRehearsals_TalkDrafts_TalkDraftCommunityId_TalkDraftId",
                        columns: x => new { x.TalkDraftCommunityId, x.TalkDraftId },
                        principalTable: "TalkDrafts",
                        principalColumns: new[] { "CommunityId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityReferences_MeetupDraftCommunityId_MeetupDraftId",
                table: "EntityReferences",
                columns: new[] { "MeetupDraftCommunityId", "MeetupDraftId" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityReferences_TalkDraftCommunityId_TalkDraftId",
                table: "EntityReferences",
                columns: new[] { "TalkDraftCommunityId", "TalkDraftId" });

            migrationBuilder.CreateIndex(
                name: "IX_MeetupDrafts_VenueId",
                table: "MeetupDrafts",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerDrafts_CompanyId",
                table: "SpeakerDrafts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_CompanyId",
                table: "Speakers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TalkDrafts_MeetupDraftCommunityId_MeetupDraftId",
                table: "TalkDrafts",
                columns: new[] { "MeetupDraftCommunityId", "MeetupDraftId" });

            migrationBuilder.CreateIndex(
                name: "IX_TalkRehearsals_TalkDraftCommunityId_TalkDraftId",
                table: "TalkRehearsals",
                columns: new[] { "TalkDraftCommunityId", "TalkDraftId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MeetupDrafts_EntityReferences_VenueId",
                table: "MeetupDrafts",
                column: "VenueId",
                principalTable: "EntityReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityReferences_MeetupDrafts_MeetupDraftCommunityId_Meetup~",
                table: "EntityReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_TalkDrafts_MeetupDrafts_MeetupDraftCommunityId_MeetupDraftId",
                table: "TalkDrafts");

            migrationBuilder.DropTable(
                name: "FriendDrafts");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "SpeakerDrafts");

            migrationBuilder.DropTable(
                name: "Speakers");

            migrationBuilder.DropTable(
                name: "TalkRehearsals");

            migrationBuilder.DropTable(
                name: "VenueDrafts");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "MeetupDrafts");

            migrationBuilder.DropTable(
                name: "EntityReferences");

            migrationBuilder.DropTable(
                name: "TalkDrafts");
        }
    }
}
