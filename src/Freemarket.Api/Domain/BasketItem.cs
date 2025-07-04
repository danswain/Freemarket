using System.ComponentModel.DataAnnotations;

namespace Freemarket.Api.Domain;

public class BasketItem
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid BasketId { get; set; }
}