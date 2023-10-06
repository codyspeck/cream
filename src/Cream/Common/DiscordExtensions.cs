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

    public static SelectMenuBuilder AddOptions<T>(
        this SelectMenuBuilder builder,
        IEnumerable<T> source,
        Func<T, (string, string)> selector)
    {
        foreach (var item in source)
        {
            var (label, value) = selector(item);
            builder.AddOption(label, value);
        }

        return builder;
    }
}