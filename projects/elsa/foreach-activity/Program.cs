using Elsa.Extensions;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;
using Elsa.Workflows.Memory;
using Elsa.Workflows.Models;

var services = new ServiceCollection();
services.AddElsa();

var serviceProvider = services.BuildServiceProvider();
var runner = serviceProvider.GetRequiredService<IWorkflowRunner>();

var counter = new Variable<int>("current", 0);
var numbers = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

var workflow = new Sequence
{
    Variables = { counter },
    Activities =
    {
        new ForEach()
        {
            Items = new Input<ICollection<object>>(numbers),
            CurrentValue = new Output<object>(counter),
            Body = new Sequence
                {
                    Activities =
                    {
                        new WriteLine(context => $"Counter {counter.Get(context)}")
                    }
                }
        }
    }
};

await runner.RunAsync(workflow);
