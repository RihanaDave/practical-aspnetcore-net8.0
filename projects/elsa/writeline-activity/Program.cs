using Elsa.Extensions;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var workflow = new WriteLine("Hello world"); //This is an Esla activity
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

await runner.RunAsync(workflow);


