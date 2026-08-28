using Microsoft.AspNetCore.Mvc;

namespace RxFlow.Api.Controllers;

[ApiController, Route("admin/lab-override")]
public sealed class LabOverrideController : ControllerBase
{
    [HttpPost]
    public IActionResult Override(LabOverrideRequest request) => Ok(new { request.OrderId, request.LabCode, Accepted = true });
}
public sealed record LabOverrideRequest(Guid OrderId, string LabCode);
