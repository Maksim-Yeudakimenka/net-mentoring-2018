using System;
using EmailSender.Bootstrap;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PostSharp.Aspects;

namespace EmailSender.CodeRewriting
{
  [Serializable]
  public class LoggerPostSharpAspect : OnMethodBoundaryAspect
  {
    private ILogger<LoggerPostSharpAspect> _logger;

    public ILogger<LoggerPostSharpAspect> Logger
    {
      get
      {
        if (_logger == null)
        {
          var applicationConfiguration = ServiceBootstrapper.ConfigureApplicationConfiguration();
          var loggerFactory = ServiceBootstrapper.ConfigureLogging(applicationConfiguration);
          _logger = loggerFactory.CreateLogger<LoggerPostSharpAspect>();
        }

        return _logger;
      }
    }

    public override void OnEntry(MethodExecutionArgs args)
    {
      var method = args.Method;
      var parameters = method.GetParameters();
      var arguments = args.Arguments;

      Logger.LogDebug(
        "Method call: {FullMethodName} with {ParameterCount} parameter(s).",
        $"{args.Instance}.{method.Name}", parameters.Length);

      for (var i = 0; i < parameters.Length; i++)
      {
        var paramName = parameters[i].Name;
        var paramValue = JsonConvert.SerializeObject(arguments[i]);

        Logger.LogDebug("{ParamName} = {ParamValue}", paramName, paramValue);
      }

      args.FlowBehavior = FlowBehavior.Default;
    }

    public override void OnSuccess(MethodExecutionArgs args)
    {
      var returnValue = JsonConvert.SerializeObject(args.ReturnValue);

      Logger.LogDebug("Return value: {ReturnValue}.", returnValue);
    }
  }
}