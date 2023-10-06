namespace Cream.Common;

public static class SerilogExtensions
{
    public static LoggerConfiguration When(
        this LoggerConfiguration configuration,
        bool condition,
        Func<LoggerConfiguration, LoggerConfiguration> fn)
    {
        return condition ? fn(configuration) : configuration;
    }
}
