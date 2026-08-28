using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
namespace RxFlow.Infrastructure.Migrations;
[DbContext(typeof(RxFlowDbContext)), Migration("202601010004_OperationsNote")]
public sealed class OperationsNote : Migration { protected override void Up(MigrationBuilder b) => b.AddColumn<string>("operations_note", "rx_orders", nullable: true); protected override void Down(MigrationBuilder b) { } }
