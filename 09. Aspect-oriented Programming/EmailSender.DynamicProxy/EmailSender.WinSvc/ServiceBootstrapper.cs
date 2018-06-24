using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace EmailSender.WinSvc
{
  public static class ServiceBootstrapper
  {
    public static IConfigurationRoot ConfigureApplicationConfiguration()
    {
      // important - the order config files are added defines their precedence
      // at the bottom is a base set of configurations that should imply a safe
      // configuration that can be used within undefined/dynamic environments
      var builder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

      return builder.Build();
    }

    public static ILoggerFactory ConfigureLogging(IConfigurationRoot configuration)
    {
      // First, set up Serilog
      LoggerConfiguration serilog = new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .ReadFrom.Configuration(configuration)
        .Enrich.FromLogContext();

      // If in release mode, check to see if the debugger is attached. In debug mode, always log to the console.
#if !DEBUG
// Configure logger to write to console if debugger is attached (allows for easy debugging from VS)
      if (Debugger.IsAttached)
#endif
      {
        serilog = serilog.WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Verbose, theme: AnsiConsoleTheme.Code);
      }

      // Link up to Microsoft.Extensions.Logging
      LoggerFactory loggerFactory = new LoggerFactory();
      loggerFactory.AddSerilog(serilog.CreateLogger());

      return loggerFactory;
    }
  }
}