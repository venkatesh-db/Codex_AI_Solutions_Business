using System.ComponentModel.DataAnnotations;

namespace RxFlow.Application;

public sealed record CreateOrderRequest(
    [Required] string PatientId,
    [Required] string FrameSku,
    decimal Sphere,
    decimal Cylinder,
    [Range(0, 180)] int Axis,
    [Required] string Material,
    [Required] string Coating);

public sealed record OrderResponse(Guid Id, string Status, string LabCode, decimal Price);
