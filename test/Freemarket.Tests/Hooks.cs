using Microsoft.AspNetCore.Mvc.Testing;
using Reqnroll;
using Reqnroll.BoDi;

namespace Freemarket.Tests;

[Binding]
public sealed class Hooks
{
    private static WebApplicationFactory<Program>? _factory;

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [BeforeScenario]
    public void BeforeScenario(IObjectContainer objectContainer)
    {
        var client = _factory!.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });
        objectContainer.RegisterInstanceAs<HttpClient>(client);
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        _factory?.Dispose();
    }
}