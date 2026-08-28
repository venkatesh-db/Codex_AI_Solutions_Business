using System.ComponentModel.DataAnnotations;
using RxFlow.Domain;

namespace RxFlow.Application;

public sealed class PrescriptionValidator
{
    public Prescription Normalize(Prescription value)
    {
        var cylinder = Math.Clamp(value.Cylinder, -6.00m, 6.00m);
        var axis = Math.Clamp(value.Axis, 0, 180);
        if (Math.Abs(value.Sphere) > 20.00m) throw new ValidationException("Sphere is outside manufacturing limits");
        return new Prescription(value.Sphere, cylinder, axis);
    }
}
