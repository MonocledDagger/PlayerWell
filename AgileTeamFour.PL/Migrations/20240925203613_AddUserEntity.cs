using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileTeamFour.PL.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblComments",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    TimePosted = table.Column<DateTime>(type: "datetime", nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblComme__C3B4DFAA7DFC26BA", x => x.CommentID);
                });

            migrationBuilder.CreateTable(
                name: "tblEvents",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false),
                    GameID = table.Column<int>(type: "int", nullable: false),
                    EventName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Server = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Platform = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblEvent__7944C8709BA90437", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "tblGames",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "int", nullable: false),
                    GameName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Platform = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Picture = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Genre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblGames__2AB897DD40D94A8C", x => x.GameID);
                });

            migrationBuilder.CreateTable(
                name: "tblPlayerEvents",
                columns: table => new
                {
                    PlayerEventID = table.Column<int>(type: "int", nullable: false),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblPlaye__B001D167570D9DB0", x => x.PlayerEventID);
                });

            migrationBuilder.CreateTable(
                name: "tblReviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false),
                    StarsOutOf5 = table.Column<int>(type: "int", nullable: false),
                    ReviewText = table.Column<string>(type: "text", nullable: true),
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    RecipientID = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblRevie__74BC79AEFD49D53C", x => x.ReviewID);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(28)", unicode: false, maxLength: 28, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IconPic = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblUsers__1788CCAC98558B53", x => x.UserID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblComments");

            migrationBuilder.DropTable(
                name: "tblEvents");

            migrationBuilder.DropTable(
                name: "tblGames");

            migrationBuilder.DropTable(
                name: "tblPlayerEvents");

            migrationBuilder.DropTable(
                name: "tblReviews");

            migrationBuilder.DropTable(
                name: "tblUsers");
        }
    }
}
