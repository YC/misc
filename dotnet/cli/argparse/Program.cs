using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddScoped<TestRunner>();
    })
    .ConfigureLogging(logging =>
    {
        logging.AddConsole();
    })
    .Build();

var rootCommand = new RootCommand("CommandLine Parsing Example");
var intArg = new Option<int>("--int-arg") { Required = true };
var stringArg = new Option<string?>("--string-arg");
rootCommand.Options.Add(intArg);
rootCommand.Options.Add(stringArg);
rootCommand.SetAction(parseResult =>
{
    var parseTest = new ParseTest
    {
        IntArg = parseResult.GetValue(intArg),
        StringArg = parseResult.GetValue(stringArg)
    };

    var testRunner = host.Services.GetRequiredService<TestRunner>();
    return testRunner.Run(parseTest);
});

var result = await rootCommand.Parse(args).InvokeAsync();

host.Dispose();
return result;

public class TestRunner(ILogger<TestRunner> logger)
{
    private readonly ILogger<TestRunner> _logger = logger;

    public int Run(ParseTest parseTest) {
        _logger.LogInformation("Int: {val}", parseTest.IntArg);
        _logger.LogInformation("String: {val}", parseTest.StringArg);
        return parseTest.IntArg;
    }
}

public class ParseTest {
    public int IntArg;
    public string? StringArg;
}
