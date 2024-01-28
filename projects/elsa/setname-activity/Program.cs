using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

var workflow = new SetNameWorkflow();
var result = await runner.RunAsync(workflow);

public class SetNameWorkflow : WorkflowBase
{
    protected override void Build(IWorkflowBuilder builder)
    {
        builder.Root = new Sequence
        {
            Activities = 
            {
                new SetName("My Simple Workflow"),
                new ShowName()
            }
        };
    } 
}

public class ShowName : Activity
{
    protected override ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var instanceName = context.WorkflowExecutionContext.GetProperty<string>("WorkflowInstanceName");
        Console.WriteLine($"WorkflowInstanceName {instanceName}");
        return ValueTask.CompletedTask;
    }
}