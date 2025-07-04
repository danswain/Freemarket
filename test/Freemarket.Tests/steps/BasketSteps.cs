using System.Text;
using Freemarket.Api.Domain;
using Reqnroll;
using Newtonsoft.Json;

namespace Freemarket.Tests;

[Binding]
public class BasketSteps
{
    private Guid _basketGuid;
    private readonly BasketApi _basketApi;

    public BasketSteps(BasketApi basketApi)
    {
        _basketApi = basketApi;
    }

    [StepDefinition("I have a basket")]
    public async Task HaveABasket()
    {
        _basketGuid = Guid.NewGuid();
    }

    [StepDefinition("I add an item to the basket")]
    public async Task AddItemToBasket()
    {
        var item = new BasketItem
        {
            Id = Guid.NewGuid(),
            Name = "A Cup of Tea"
        };
        StringContent httpContent = ToHttpContent(item);
        var response = await _basketApi.Client.PutAsync($"Basket/{_basketGuid}", httpContent);
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }

    [StepDefinition("I have (.*) item in the basket")]
    public async Task GetItemsInBasket(int numberOfItems)
    {
        var response = await _basketApi.Client.GetAsync($"Basket/{_basketGuid}");
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        var basket = JsonConvert.DeserializeObject<Basket>(json);
        Assert.That(basket.BasketItems, Is.Not.Null.And.Not.Empty, "Basket is null or empty");
        
    }

    private static StringContent ToHttpContent(dynamic item)
    {
        string jsonPayload = JsonConvert.SerializeObject(item);
        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        return httpContent;
    }
}