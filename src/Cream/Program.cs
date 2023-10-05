using System.Reflection;
using Cream;
using Cream.Framework;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Victoria;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var configuration = new ConfigurationBuilder()
    .AddJsonFile(Constants.AppSettings, optional: true)
    .AddEnvironmentVariables()
    .Build()
    .Get<CreamConfiguration>();
    
await using var provider = new ServiceCollection()
    .AddLogging(lb => lb.AddSerilog(Log.Logger, dispose: true))
    .AddSerilog(Log.Logger)
    .AddDiscordSocketClient()
    .AddLavaNode()
    .AddMediatR(m => m
        .RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
    .AddSingleton<CommandService>()
    .BuildServiceProvider()
    .RegisterDiscordSocketClientMessages();

var client = provider.GetRequiredService<DiscordSocketClient>();

await client.LoginAsync(TokenType.Bot, configuration!.Token);
await client.StartAsync();

await Task.Delay(-1);
