using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http.Extensions;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.Run();

[ApiController]
public class HomeController : Controller
{
    [HttpGet("")]
    public ActionResult<string> Index()
    {
        try
        {
            throw new ApplicationException("Catch this one");
        }
        catch (Exception ex)
        {
            return this.ApiProblemDetails(HttpStatusCode.InternalServerError, exception: ex);
        }
    }
}




public class ApiProblemsDetails : ProblemDetails
{
    public string RequestPath { get; set; }

    public object RequestPayload { get; set; }

    public IReadOnlyDictionary<string, string> ValidationMessages { get; set; } = new Dictionary<string, string>();

    public string StackTrace { get; set; }
}

public static class ControllerExtensions
{
    public static ActionResult ApiProblemDetails(this Controller self, HttpStatusCode statusCode,
        string title = "",
        string detail = "",
        IReadOnlyDictionary<string, string> validationMessages = null,
        ModelStateDictionary modelState = null,
        Exception exception = null,
        object request = null)
    {
        var details = new ApiProblemsDetails
        {
            Title = title,
            Detail = detail,
            RequestPath = self.Request.GetDisplayUrl(),
            Status = (int)statusCode
        };

        if (validationMessages != null)
            details.ValidationMessages = validationMessages;

        if (modelState != null)
        {
            var modelStateMessages = modelState
            .Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => string.Join(",", kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
            );

            if (details.ValidationMessages == null)
                details.ValidationMessages = modelStateMessages;
            else
                details.ValidationMessages = details.ValidationMessages.Union(modelStateMessages).ToDictionary(x => x.Key, x => x.Value);
        }

        var host = self.HttpContext.RequestServices.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;

        if (exception != null && host.EnvironmentName == Environments.Development)
        {
            if (string.IsNullOrWhiteSpace(details.Detail))
                details.Detail = "Exception Message: " + exception.Message;
            else
                details.Detail += "\n\n Exception Message: " + exception.Message;

            details.StackTrace = exception.StackTrace;
        }

        if (request != null)
            details.RequestPayload = request;

        return self.StatusCode((int)statusCode, details);
    }
}
