using System.CommandLine;
using System.CommandLine.Binding;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

var builder = new CommandLineBuilder(new TestCommand())
    .UseDefaults()
    .UseHost(host => {
        host
        .ConfigureLogging(logging => {
            logging.AddConsole();
            logging.AddFilter<ConsoleLoggerProvider>("Microsoft.Hosting.Lifetime", LogLevel.Error);
        })
        .ConfigureServices(services => {
            services.AddScoped<TestRunner>();
        })
        .UseCommandHandler<TestCommand, TestHandler>();
    });

var parser = builder.Build();
await parser.InvokeAsync(args);

public class TestCommand : RootCommand {
    public TestCommand()
    {
        var intArg = new Option<int>("--int-arg") { IsRequired = true };
        var stringArg = new Option<string?>("--string-arg");
        this.Description = "CommandLine Parsing Example";
        this.AddOption(intArg);
        this.AddOption(stringArg);
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

public class TestHandler(TestRunner runner) : ICommandHandler {
    private TestRunner _runner = runner;

    public int IntArg { get; set; } = 0;
    public string StringArg { get; set; } = "";

    public int Invoke(InvocationContext context)
    {
        throw new NotImplementedException();
    }

    public Task<int> InvokeAsync(InvocationContext context) {
        // TODO: ParseTest directly? https://github.com/dotnet/command-line-api/issues/1858
        _runner.Run(new ParseTest { IntArg = IntArg, StringArg = StringArg });
        return Task.FromResult<int>(0);
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
