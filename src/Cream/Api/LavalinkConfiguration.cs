namespace Cream.Api;

public record LavalinkConfiguration(Uri Host, string Password)
{
    public const string ConfigurationKey = "Lavalink";
}
