using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddScoped<Test>();
    })
    .ConfigureLogging(logging => {
        logging.AddConsole();
    })
    .ConfigureAppConfiguration(config => {
        config.Sources.Clear();
        config.SetBasePath(AppContext.BaseDirectory);
        config.AddJsonFile("appsettings.json");
    })
    .Build();

host.Services.GetRequiredService<Test>().Run();

Console.Out.Flush();
Console.Error.Flush();
