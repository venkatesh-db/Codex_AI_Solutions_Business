using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
namespace RxFlow.Infrastructure.Migrations;
[DbContext(typeof(RxFlowDbContext))]
public sealed class RxFlowDbContextModelSnapshot : ModelSnapshot { protected override void BuildModel(ModelBuilder modelBuilder) { modelBuilder.HasAnnotation("ProductVersion", "8.0.11"); modelBuilder.Entity("RxFlow.Domain.Order", b => { b.Property<Guid>("Id"); b.HasKey("Id"); b.ToTable("rx_orders"); }); } }
