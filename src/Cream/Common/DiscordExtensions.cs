using Discord;

namespace Cream.Common;

public static class DiscordExtensions
{
    public static EmbedBuilder AddFields<T>(
        this EmbedBuilder builder,
        IEnumerable<T> source,
        Func<T, string> nameSelector,
        Func<T, object> valueSelector)
    {
        foreach (var item in source)
            builder.AddField(nameSelector(item), valueSelector(item));

        return builder;
    }
    
    public static EmbedBuilder When(this EmbedBuilder builder, bool condition, Func<EmbedBuilder, EmbedBuilder> fn)
    {
        return condition ? fn(builder) : builder;
    }
}