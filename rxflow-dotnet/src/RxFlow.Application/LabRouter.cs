using RxFlow.Domain;

namespace RxFlow.Application;

public sealed class LabRouter
{
    private readonly Lab[] labs = [new("LAB-EAST", 1, 20), new("LAB-WEST", 2, 100)];
    public Lab Choose(string material, string coating) => labs.OrderBy(lab => lab.Priority).First();
}
