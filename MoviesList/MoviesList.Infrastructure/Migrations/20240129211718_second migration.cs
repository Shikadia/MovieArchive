using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesList.Infrastructure.Migrations
{
    public partial class secondmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Actors_ActorId1",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_ActorId1",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "ActorId1",
                table: "Ratings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActorId",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ActorId1",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ActorId1",
                table: "Ratings",
                column: "ActorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Actors_ActorId1",
                table: "Ratings",
                column: "ActorId1",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
