using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileTeamFour.PL.Migrations
{
    /// <inheritdoc />
    public partial class AddFriendsAndGuilds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__tblUsers__1788CCAC98558B53",
                table: "tblUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tblRevie__74BC79AEFD49D53C",
                table: "tblReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tblPlaye__B001D167570D9DB0",
                table: "tblPlayerEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tblGames__2AB897DD40D94A8C",
                table: "tblGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tblEvent__7944C8709BA90437",
                table: "tblEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tblComme__C3B4DFAA7DFC26BA",
                table: "tblComments");

            migrationBuilder.AlterColumn<string>(
                name: "IconPic",
                table: "tblUsers",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "tblUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessLevel",
                table: "tblUsers",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "tblEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK__tblUsers__1788CCAC399AA47B",
                table: "tblUsers",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__tblRevie__74BC79AEDA07C2F6",
                table: "tblReviews",
                column: "ReviewID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__tblPlaye__B001D1671C924856",
                table: "tblPlayerEvents",
                column: "PlayerEventID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__tblGames__2AB897DD74CB37B9",
                table: "tblGames",
                column: "GameID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__tblEvent__7944C870BC36FDB2",
                table: "tblEvents",
                column: "EventID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__tblComme__C3B4DFAABEDCE7AF",
                table: "tblComments",
                column: "CommentID");

            migrationBuilder.CreateTable(
                name: "tblPlayers",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IconPic = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblPlaye__4A4E74A82290DE9C", x => x.PlayerID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tblUsers__1788CCAC399AA47B",
                table: "tblUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tblRevie__74BC79AEDA07C2F6",
                table: "tblReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tblPlaye__B001D1671C924856",
                table: "tblPlayerEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tblGames__2AB897DD74CB37B9",
                table: "tblGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tblEvent__7944C870BC36FDB2",
                table: "tblEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tblComme__C3B4DFAABEDCE7AF",
                table: "tblComments");

            migrationBuilder.DropColumn(
                name: "AccessLevel",
                table: "tblUsers");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "tblEvents");

            migrationBuilder.AlterColumn<string>(
                name: "IconPic",
                table: "tblUsers",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "tblUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK__tblUsers__1788CCAC98558B53",
                table: "tblUsers",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__tblRevie__74BC79AEFD49D53C",
                table: "tblReviews",
                column: "ReviewID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__tblPlaye__B001D167570D9DB0",
                table: "tblPlayerEvents",
                column: "PlayerEventID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__tblGames__2AB897DD40D94A8C",
                table: "tblGames",
                column: "GameID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__tblEvent__7944C8709BA90437",
                table: "tblEvents",
                column: "EventID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__tblComme__C3B4DFAA7DFC26BA",
                table: "tblComments",
                column: "CommentID");
        }
    }
}
