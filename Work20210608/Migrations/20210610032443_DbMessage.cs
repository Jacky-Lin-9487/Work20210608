using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Work20210608.Migrations
{
    public partial class DbMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Member");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "Members");

            migrationBuilder.AddColumn<string>(
                name: "Account",
                table: "Members",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Members",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "MemberId");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(maxLength: 50, nullable: false),
                    Content = table.Column<string>(maxLength: 50, nullable: true),
                    Time = table.Column<DateTime>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MemberId",
                table: "Messages",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Account",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Member");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Member",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "MemberId");
        }
    }
}
