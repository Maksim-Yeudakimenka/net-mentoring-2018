using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmailSender.DynamicProxy
{
  public class LoggingInterceptor : IInterceptor
  {
    private readonly ILogger _logger;

    public LoggingInterceptor(ILogger logger)
    {
      _logger = logger;
    }

    public void Intercept(IInvocation invocation)
    {
      var method = invocation.Method;
      var parameters = method.GetParameters();
      var arguments = invocation.Arguments;

      _logger.LogDebug(
        "Method call: {FullMethodName} with {ParameterCount} parameter(s).",
        $"{invocation.InvocationTarget}.{method.Name}", parameters.Length);

      for (var i = 0; i < parameters.Length; i++)
      {
        var paramName = parameters[i].Name;
        var paramValue = JsonConvert.SerializeObject(arguments[i]);

        _logger.LogDebug("{ParamName} = {ParamValue}", paramName, paramValue);
      }

      invocation.Proceed();

      var returnValue = JsonConvert.SerializeObject(invocation.ReturnValue);

      _logger.LogDebug("Return value: {ReturnValue}.", returnValue);
    }
  }
}