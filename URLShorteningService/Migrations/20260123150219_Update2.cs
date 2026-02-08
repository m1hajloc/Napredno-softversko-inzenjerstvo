using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShorteningService.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlEntityClick",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlEntityId = table.Column<int>(type: "int", nullable: false),
                    ClickedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlEntityClick", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrlEntityClick_UrlEntity_UrlEntityId",
                        column: x => x.UrlEntityId,
                        principalTable: "UrlEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlEntityClick_UrlEntityId",
                table: "UrlEntityClick",
                column: "UrlEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlEntityClick");
        }
    }
}
