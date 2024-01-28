using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapDefaultControllerRoute();
app.Run();

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return new ContentResult
        {
            Content = @"
            <html><body>
            <b>Hello World running on Endpoint Routing</b>
            <p>As you can see, all the existing routing methods <b>just works</b>.
            Now they are just <a href=""https://blogs.msdn.microsoft.com/webdev/2018/08/27/asp-net-core-2-2-0-preview1-endpoint-routing/"">faster</a>. We will explore the cool stuff that Endpoint Routing brings in the next samples.
            <br /><br />
                <li><a href=""/"">/</a></li>
                <li><a href=""/home"">/ home</a </li>
                <li><a href=""/home/index"">/home/index</a></li>
            </ul>
            </body></html> ",
            ContentType = "text/html"
        };
    }
}

