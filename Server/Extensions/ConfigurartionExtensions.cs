using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Server.Extensions;

public static class ConfigurationExtensions
{
    public static LogLevel GetLogLevel(this IConfiguration configuration)
    {
        var configurationValue = configuration["Logging:LogLevel:Default"];
        
        LogLevel logLevel;
        var parseResult = LogLevel.TryParse(configurationValue, out logLevel);
        if(parseResult == false)
            throw new InvalidOperationException();
        
        return logLevel;
    }
}