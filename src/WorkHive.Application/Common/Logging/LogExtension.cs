using Microsoft.Extensions.Logging;

namespace WorkHive.Application.Common.Logging;
public static partial class Log
{
    [LoggerMessage(101, LogLevel.Error, "Error while executing query {exception}")]
    public static partial void ErrorWhileExecutingQuery(this ILogger logger, Exception exception);
}