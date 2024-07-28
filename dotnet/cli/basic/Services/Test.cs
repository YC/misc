using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class Test(ILogger<Test> logger, IConfiguration configuration)
{
    private readonly ILogger<Test> _logger = logger;
    private readonly IConfiguration _configuration = configuration;

    public void Run() {
        var testvarValue = _configuration["testvar"];
        _logger.LogInformation("testvar value from configuration is {testvarValue}", testvarValue);
        Console.WriteLine("Test");
    }
}
