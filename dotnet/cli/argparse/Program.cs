using System.CommandLine;
using System.CommandLine.Binding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddScoped<TestRunner>();
        services.AddScoped<TestCommand>();
    })
    .ConfigureLogging(logging =>
    {
        logging.AddConsole();
    })
    .Build();

await host.Services.GetRequiredService<TestCommand>().InvokeAsync(args);
host.Dispose();

public class TestCommand : RootCommand {
    private readonly TestRunner _runner;

    public TestCommand(TestRunner runner)
    {
        _runner = runner;

        var intArg = new Option<int>("--int-arg") { IsRequired = true };
        var stringArg = new Option<string?>("--string-arg");
        this.Description = "CommandLine Parsing Example";
        this.AddOption(intArg);
        this.AddOption(stringArg);
        this.SetHandler(Execute, new ParseTestBinder(intArg, stringArg));
    }

    private void Execute(ParseTest test) {
        _runner.Run(test);
    }
}

public class TestRunner(ILogger<TestRunner> logger)
{
    private readonly ILogger<TestRunner> _logger = logger;

    public void Run(ParseTest parseTest) {
        _logger.LogInformation("Int: {val}", parseTest.IntArg);
        _logger.LogInformation("String: {val}", parseTest.StringArg);
    }
}

public class ParseTest {
    public int IntArg;
    public string? StringArg;
}

public class ParseTestBinder : BinderBase<ParseTest> {
    private readonly Option<int> _intArg;
    private readonly Option<string?> _stringArg;

    public ParseTestBinder(Option<int> intArg, Option<string?> stringArg) {
        _intArg = intArg;
        _stringArg = stringArg;
    }

    protected override ParseTest GetBoundValue(BindingContext bindingContext)
    {
        return new ParseTest {
            IntArg = bindingContext.ParseResult.GetValueForOption(_intArg),
            StringArg = bindingContext.ParseResult.GetValueForOption(_stringArg)
        };
    }
}
