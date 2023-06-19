using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsWebsiteAPI.Migrations
{
    /// <inheritdoc />
    public partial class newsANDAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4da5803-2608-48c6-927f-d3f6004849bd");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SecurityID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_AspNetUsers_SecurityID",
                        column: x => x.SecurityID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    OurNews = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_AspNetUsers_SecurityID",
                        column: x => x.SecurityID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_News_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cd6b7d8e-b8ea-459d-bc66-4a5e1ed63bdf", "1", "Admin", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_SecurityID",
                table: "Authors",
                column: "SecurityID");

            migrationBuilder.CreateIndex(
                name: "IX_News_AuthorID",
                table: "News",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_News_SecurityID",
                table: "News",
                column: "SecurityID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd6b7d8e-b8ea-459d-bc66-4a5e1ed63bdf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e4da5803-2608-48c6-927f-d3f6004849bd", "1", "Admin", "Admin" });
        }
    }
}
