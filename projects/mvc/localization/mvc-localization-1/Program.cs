using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder();
builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();

var supportedCultures = new List<CultureInfo>
{
    new CultureInfo("fr-FR")
};

var options = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("fr-FR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

app.UseRequestLocalization(options);

app.MapDefaultControllerRoute();
app.Run();


namespace MvcLocalization
{
    public class HomeController : Controller
    {
        readonly IStringLocalizer<HomeController> _local;

        public HomeController(IStringLocalizer<HomeController> local)
        {
            _local = local;
        }
        public ActionResult Index()
        {
            var culture = this.HttpContext.Features.Get<IRequestCultureFeature>();

            return new ContentResult
            {
                Content = $@"<html><body>
                    <h1>MVC Localization Resource File Naming - Dot Naming Convention</h1>
                    <p>
                    <i><b>Resources are named for the full type name of their class minus the assembly name</b>. For example, a French resource in a project whose main assembly is LocalizationWebsite.Web.dll for the class LocalizationWebsite.Web.Startup would be named Startup.fr.resx. A resource for the class LocalizationWebsite.Web.Controllers.HomeController would be named Controllers.HomeController.fr.resx. If your targeted class's namespace isn't the same as the assembly name you will need the full type name. For example, in the sample project a resource for the type ExtraNamespace.Tools would be named ExtraNamespace.Tools.fr.resx.
                    </i>
                    <a href=""https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-3.1"">Doc</a>
                    </p>
                    <p>
                        In this sample, the assembly name is ""mvc-localization"" however the namespace used is ""MvcLocalization"". Since the namespace differs from the Assembly Name, we use full type path.
                    </p>
                    <b>Culture requested</b> {culture.RequestCulture.Culture} <br/>
                    <b>UI Culture requested</b> {culture.RequestCulture.UICulture} <br/>
                    Text: {_local["Hello"]}<br/>
                    Text: {_local["Goodbye"]}</body></html>",
                ContentType = "text/html"
            };
        }
    }
}