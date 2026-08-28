namespace RxFlow.Domain;

public sealed class Order
{
    public Guid Id { get; set; }
    public required string PatientId { get; set; }
    public required string FrameSku { get; set; }
    public decimal Sphere { get; set; }
    public decimal Cylinder { get; set; }
    public int Axis { get; set; }
    public required string Material { get; set; }
    public required string Coating { get; set; }
    public decimal Price { get; set; }
    public required string Status { get; set; }
    public required string LabCode { get; set; }
    public DateTimeOffset SubmittedAt { get; set; }
    public override string ToString() => $"Order {{ Id = {Id}, PatientId = {PatientId}, Sphere = {Sphere}, Cylinder = {Cylinder}, Axis = {Axis}, Status = {Status} }}";
}

public sealed record Prescription(decimal Sphere, decimal Cylinder, int Axis);
public sealed record Lab(string Code, int Priority, int Capacity);
public sealed record OrderSubmitted(Guid OrderId, string PatientId, decimal Sphere, decimal Cylinder, int Axis, string LabCode);
