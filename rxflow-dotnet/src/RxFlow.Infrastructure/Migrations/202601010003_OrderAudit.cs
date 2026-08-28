using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
namespace RxFlow.Infrastructure.Migrations;
[DbContext(typeof(RxFlowDbContext)), Migration("202601010003_OrderAudit")]
public sealed class OrderAudit : Migration { protected override void Up(MigrationBuilder b) => b.CreateTable("order_audit", columns: t => new { id = t.Column<long>(nullable: false).Annotation("Npgsql:ValueGenerationStrategy", 1), order_id = t.Column<Guid>(nullable: false), payload = t.Column<string>(nullable: false) }, constraints: c => c.PrimaryKey("PK_order_audit", x => x.id)); protected override void Down(MigrationBuilder b) => b.DropTable("order_audit"); }
