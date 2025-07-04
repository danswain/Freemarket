using Freemarket.Api.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Freemarket.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BasketController : ControllerBase
{
    [HttpGet("{basketGuid:guid}",Name = "GetBasket")]
    public async Task<IActionResult> GetBasket([FromRoute] Guid basketGuid)
    {
        var basket = new Basket();
        return Ok(basket);
    }
    
    [HttpPut("{basketGuid:guid}",Name = "PutBasket")]
    public async Task<IActionResult> PutBasket([FromRoute] Guid basketGuid, dynamic jsonPayload)
    {
        return Ok(jsonPayload);
    }    
}