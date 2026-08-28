using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RxFlow.Infrastructure.Migrations;
[DbContext(typeof(RxFlowDbContext)), Migration("202601010001_InitialOrders")]
public sealed class InitialOrders : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.CreateTable("rx_orders", columns: table => new { Id = table.Column<Guid>(nullable: false), patient_id = table.Column<string>(nullable: false), frame_sku = table.Column<string>(nullable: false), Sphere = table.Column<decimal>(nullable: false), Cylinder = table.Column<decimal>(nullable: false), Axis = table.Column<int>(nullable: false), Material = table.Column<string>(nullable: false), Coating = table.Column<string>(nullable: false), Price = table.Column<decimal>(nullable: false), Status = table.Column<string>(nullable: false), lab_code = table.Column<string>(nullable: false), submitted_at = table.Column<DateTimeOffset>(nullable: false) }, constraints: constraints => constraints.PrimaryKey("PK_rx_orders", x => x.Id));
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable("rx_orders");
}
