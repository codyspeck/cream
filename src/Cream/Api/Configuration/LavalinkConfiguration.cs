namespace Cream.Api.Configuration;

public record LavalinkConfiguration(Uri Host, string Password)
{
    public const string ConfigurationKey = "Lavalink";
}
