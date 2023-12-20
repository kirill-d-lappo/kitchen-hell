using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenHell.Persistence.Orders.Migrations
{
    /// <inheritdoc />
    public partial class RestId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RestaurantId",
                schema: "orders",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantId",
                schema: "orders",
                table: "Orders");
        }
    }
}
