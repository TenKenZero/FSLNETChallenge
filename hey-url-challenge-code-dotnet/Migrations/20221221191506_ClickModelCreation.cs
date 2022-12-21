using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace heyurlchallengecodedotnet.Migrations
{
    /// <inheritdoc />
    public partial class ClickModelCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Urls",
                newName: "ID");

            migrationBuilder.AlterColumn<string>(
                name: "ShortUrl",
                table: "Urls",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateCreation",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullUrl",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Clicks",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clicks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Clicks_Urls_UrlID",
                        column: x => x.UrlID,
                        principalTable: "Urls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Urls_ShortUrl",
                table: "Urls",
                column: "ShortUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clicks_UrlID",
                table: "Clicks",
                column: "UrlID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clicks");

            migrationBuilder.DropIndex(
                name: "IX_Urls_ShortUrl",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "DateCreation",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "FullUrl",
                table: "Urls");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Urls",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "ShortUrl",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);
        }
    }
}
