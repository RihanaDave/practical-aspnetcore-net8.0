using Microsoft.AspNetCore.Mvc;
using Microsoft.SyndicationFeed;

var builder = WebApplication.CreateBuilder();
builder.Services.AddMvc(options =>
{
    options.OutputFormatters.Add(new RssOutputFormatter());
});

var app = builder.Build();
app.MapDefaultControllerRoute();
app.Run();

public class HomeController : Controller
{
    public ActionResult Index()
    {
        var item = new SyndicationItem()
        {
            Title = "Rss Writer Available",
            Description = "The new Rss Writer is now available as a NuGet Package!",
            Id = "https://www.nuget.org/packages/Microsoft.SyndicationFeed.ReaderWriter",
            Published = DateTimeOffset.UtcNow
        };

        item.AddCategory(new SyndicationCategory("Technology"));
        item.AddContributor(new SyndicationPerson("test", "test@mail.com"));

        var item2 = new SyndicationItem()
        {
            Title = "We need RSS 'frame'",
            Description = "We need a structure that hold the RSS/feed information",
            Id = "xx",
            Published = DateTimeOffset.UtcNow
        };

        return Ok(new[] { item, item2 });
    }
}

