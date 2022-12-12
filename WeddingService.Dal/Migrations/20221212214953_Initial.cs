using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingService.Dal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseService",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_BaseService_Id",
                        column: x => x.Id,
                        principalTable: "BaseService",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ceremonies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ceremonies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ceremonies_BaseService_Id",
                        column: x => x.Id,
                        principalTable: "BaseService",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clothes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clothes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clothes_BaseService_Id",
                        column: x => x.Id,
                        principalTable: "BaseService",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BaseServiceEntityOrder",
                columns: table => new
                {
                    OrdersId = table.Column<long>(type: "bigint", nullable: false),
                    ServicesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseServiceEntityOrder", x => new { x.OrdersId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_BaseServiceEntityOrder_BaseService_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "BaseService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseServiceEntityOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseServiceEntityOrder_ServicesId",
                table: "BaseServiceEntityOrder",
                column: "ServicesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseServiceEntityOrder");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Ceremonies");

            migrationBuilder.DropTable(
                name: "Clothes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "BaseService");
        }
    }
}
