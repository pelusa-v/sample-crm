using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sample_crm.Data.Migrations
{
    /// <inheritdoc />
    public partial class Flowowner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Flows",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flows_OwnerId",
                table: "Flows",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flows_AspNetUsers_OwnerId",
                table: "Flows",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flows_AspNetUsers_OwnerId",
                table: "Flows");

            migrationBuilder.DropIndex(
                name: "IX_Flows_OwnerId",
                table: "Flows");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Flows");
        }
    }
}
