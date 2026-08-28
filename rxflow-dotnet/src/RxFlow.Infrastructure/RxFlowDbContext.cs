using Microsoft.EntityFrameworkCore;
using RxFlow.Domain;

namespace RxFlow.Infrastructure;

public sealed class RxFlowDbContext(DbContextOptions<RxFlowDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var order = modelBuilder.Entity<Order>(); order.ToTable("rx_orders"); order.HasKey(x => x.Id);
        order.Property(x => x.PatientId).HasColumnName("patient_id"); order.Property(x => x.FrameSku).HasColumnName("frame_sku");
        order.Property(x => x.LabCode).HasColumnName("lab_code"); order.Property(x => x.SubmittedAt).HasColumnName("submitted_at");
    }
}

public sealed class OrderRepository(RxFlowDbContext database) : IOrderRepository
{
    public async Task AddAsync(Order order, CancellationToken cancellationToken) { database.Add(order); await database.SaveChangesAsync(cancellationToken); }
    public Task<Order?> FindAsync(Guid id, CancellationToken cancellationToken) => database.Orders.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    public async Task SaveAsync(Order order, CancellationToken cancellationToken) { database.Update(order); await database.SaveChangesAsync(cancellationToken); }
}
