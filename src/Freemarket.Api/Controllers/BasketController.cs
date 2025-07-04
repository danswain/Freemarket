using Freemarket.Api.Data;
using Freemarket.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freemarket.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BasketController(BasketDbContext context) : ControllerBase
{
    private readonly BasketDbContext _context = context;

    [HttpGet("{basketGuid:guid}",Name = "GetBasket")]
    public async Task<IActionResult> GetBasket([FromRoute] Guid basketGuid)
    {
        var basket = _context.Baskets
            .Include(x=>x.BasketItems)
            .FirstOrDefault(b => b.Id == basketGuid);
        
        if (basket == null) return NotFound(basketGuid);
        
        return Ok(basket);
    }
    
    [HttpPut("{basketGuid:guid}",Name = "PutBasket")]
    public async Task<IActionResult> PutBasket([FromRoute] Guid basketGuid, BasketItem basketItem)
    {
        var basket = _context.Baskets.FirstOrDefault(x => x.Id == basketGuid);
        if (basket == null)
        {
            basket = new Basket()
            {
                Id = basketGuid,
                Name = "A Nice  Basket",
                BasketItems = new List<BasketItem>()
                {
                    basketItem
                }
            };
            await _context.Baskets.AddAsync(basket);
        }
        else
        {
            basketItem.BasketId = basketGuid;
            await _context.BasketItems.AddAsync(basketItem);
        }

        await _context.SaveChangesAsync();
        
        
        return Ok(basketItem);
    }    
}