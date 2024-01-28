using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapDefaultControllerRoute();
app.Run();

public enum AlertType
{
    Success,
    Warning,
    Info,
    Danger
}

[HtmlTargetElement("danger")]
public class DangerTagHelper : TagHelper
{
    public override void Init(TagHelperContext context)
    {
        context.Items["AlertType"] = AlertType.Danger;
    }
}

[HtmlTargetElement("warning")]
public class WarningTagHelper : TagHelper
{
    public override void Init(TagHelperContext context)
    {
        context.Items["AlertType"] = AlertType.Warning;
    }
}

[HtmlTargetElement("alert")]
public class AlertHelper : TagHelper
{
    AlertType _type { get; set; } = AlertType.Info;

    public override void Init(TagHelperContext context)
    {
        if (context.Items.ContainsKey("AlertType"))
        {
            _type = (AlertType)context.Items["AlertType"];
        }
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.Add("class", $"alert {GetAlertType()}");
    }

    string GetAlertType()
    {
        switch (_type)
        {
            case AlertType.Success: return "alert-success";
            case AlertType.Warning: return "alert-warning";
            case AlertType.Info: return "alert-info";
            case AlertType.Danger: return "alert-danger";
            default: throw new System.ArgumentOutOfRangeException();
        }
    }
}

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View("Index");
    }
}
