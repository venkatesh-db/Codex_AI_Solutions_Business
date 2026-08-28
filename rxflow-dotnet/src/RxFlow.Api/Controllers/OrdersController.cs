using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RxFlow.Application;

namespace RxFlow.Api.Controllers;

[ApiController, Route("orders"), Authorize]
public sealed class OrdersController(OrderService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> Create(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var result = await service.SubmitAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }
    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id) => Ok(new { id });
}
