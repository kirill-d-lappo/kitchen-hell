using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenHell.Persistence.Orders.Migrations
{
    /// <inheritdoc />
    public partial class Statuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryStatus",
                schema: "orders",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                schema: "orders",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantStatus",
                schema: "orders",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryStatus",
                schema: "orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                schema: "orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RestaurantStatus",
                schema: "orders",
                table: "Orders");
        }
    }
}
