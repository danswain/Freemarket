namespace Freemarket.Api.Domain;

public class Basket
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<string> BasketItems { get; set; }
}