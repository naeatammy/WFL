using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBL3.Migrations
{
    /// <inheritdoc />
    public partial class upJobandFav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "freelancerID",
                table: "job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "freelancerID",
                table: "Favourite",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "freelancerID",
                table: "job");

            migrationBuilder.DropColumn(
                name: "freelancerID",
                table: "Favourite");
        }
    }
}
