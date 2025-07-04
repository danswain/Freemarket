namespace Freemarket.Tests;

public class BasketApi(HttpClient client)
{
    public HttpClient Client { get; private set; } = client;
    
    public HttpResponseMessage? Response { get; set; }
}