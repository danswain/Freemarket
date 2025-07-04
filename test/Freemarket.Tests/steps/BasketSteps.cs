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
    private Guid _basketItemId;

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
        _basketItemId = Guid.NewGuid();
        var item = new BasketItem
        {
            Id = _basketItemId,
            Name = "A Cup of Tea"
        };
        StringContent httpContent = ToHttpContent(item);
        var response = await _basketApi.Client.PutAsync($"Basket/{_basketGuid}", httpContent);
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }     
    
    [StepDefinition("I add the same item twice to the basket")]
    public async Task AddSameItemTwiceToBasket()
    {
        _basketItemId = Guid.NewGuid();
        var firstItem = new BasketItem
        {
            Name = "A Cup of Tea"
        };
        var firstResponse = await _basketApi.Client.PutAsync($"Basket/{_basketGuid}", ToHttpContent(firstItem));
        Assert.That(firstResponse.IsSuccessStatusCode, Is.True);
        
        var secondItem = new BasketItem
        {
            Name = "A Cup of Tea"
        };        
        var secondResponse = await _basketApi.Client.PutAsync($"Basket/{_basketGuid}", ToHttpContent(secondItem));
        Assert.That(secondResponse.IsSuccessStatusCode, Is.True);        
    }    
    
    [StepDefinition("I remove an item to the basket")]
    public async Task RemoveItemFromBasket()
    {
        var response = await _basketApi.Client.DeleteAsync($"Basket/{_basketGuid}/{_basketItemId}");
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }

    [StepDefinition("I have (.*) item in the basket")]
    public async Task GetItemsInBasket(int numberOfItems)
    {
        var response = await _basketApi.Client.GetAsync($"Basket/{_basketGuid}");
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        var basket = JsonConvert.DeserializeObject<Basket>(json);
        Assert.That(basket.BasketItems, Is.Not.Null, "Basket is null");
        Assert.That(basket.BasketItems.Count, Is.EqualTo(numberOfItems), "Basket has wrong number of items");
        
    }

    private static StringContent ToHttpContent(dynamic item)
    {
        string jsonPayload = JsonConvert.SerializeObject(item);
        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        return httpContent;
    }
}