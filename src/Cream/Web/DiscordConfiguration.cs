namespace Cream.Web;

public record DiscordConfiguration(string Token, string Prefix)
{
    public const string ConfigurationKey = "Discord";
}
