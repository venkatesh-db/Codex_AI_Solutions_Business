using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
namespace RxFlow.Infrastructure.Migrations;
[DbContext(typeof(RxFlowDbContext)), Migration("202601010002_Labs")]
public sealed class Labs : Migration { protected override void Up(MigrationBuilder b) => b.CreateTable("labs", columns: t => new { code = t.Column<string>(nullable: false), priority = t.Column<int>(nullable: false), capacity = t.Column<int>(nullable: false) }, constraints: c => c.PrimaryKey("PK_labs", x => x.code)); protected override void Down(MigrationBuilder b) => b.DropTable("labs"); }
