using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetManager.Migrations
{
    /// <inheritdoc />
    public partial class LinkVehiclesToMechanics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MechanicId",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_MechanicId",
                table: "Vehicles",
                column: "MechanicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_MechanicId",
                table: "Vehicles",
                column: "MechanicId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_MechanicId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_MechanicId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "MechanicId",
                table: "Vehicles");
        }
    }
}
