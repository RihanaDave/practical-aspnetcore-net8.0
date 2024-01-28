using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build(); 

app.MapControllers();
app.Run();


[Route("[controller]")]
public class HomeController : Controller
{
    [HttpGet("/")]
    [HttpGet("")] //You have to do this otherwise /Home/Index won't work
    public ActionResult Index()
    {
        return new ContentResult
        {
            Content = @"
                <html><body>
                <h1>[controller] replacement token examples</h1>
                <ul>
                    <li><a href=""/"">/</a></li>
                    <li><a href=""/home/"">/home/</a></li>
                    <li><a href=""/home/about"">/home/about</a></li>
                    <li><a href=""/about"">/about</a></li>
                </ul>
                </body></html>",
            ContentType = "text/html"
        };
    }

    [HttpGet("about")]
    public ActionResult About()
    {
        return new ContentResult
        {
            Content = @"
                <html><body>
                <b>About Page</b
                </body></html>",
            ContentType = "text/html"
        };
    }

    [HttpGet("/about")]
    public ActionResult About2()
    {
        return new ContentResult
        {
            Content = @"
                <html><body>
                <b>About Page 2</b
                </body></html>",
            ContentType = "text/html"
        };
    }
}


