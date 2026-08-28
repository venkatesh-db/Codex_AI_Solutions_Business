using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
namespace RxFlow.Infrastructure.Migrations;
[DbContext(typeof(RxFlowDbContext)), Migration("202601010005_StatusIndex")]
public sealed class StatusIndex : Migration { protected override void Up(MigrationBuilder b) => b.CreateIndex("IX_rx_orders_Status", "rx_orders", "Status"); protected override void Down(MigrationBuilder b) => b.DropIndex("IX_rx_orders_Status", "rx_orders"); }
