using Microsoft.Extensions.Logging;

namespace WorkHive.Domain.Log;
public static partial class Log
{
    [LoggerMessage(0, LogLevel.Error, "Error while trying to save changes {exception}")]    
    public static partial void ErrorWhileSavingChanges(this ILogger logger, Exception exception);
}