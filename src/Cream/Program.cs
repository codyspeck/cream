using System.Reflection;
using Cream;
using Cream.Common;
using Cream.Framework;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Victoria;

var builder = Host.CreateApplicationBuilder();

var configuration = builder.Configuration.Get<CreamConfiguration>();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .When(!string.IsNullOrWhiteSpace(configuration.SeqHost), cfg => cfg.WriteTo.Seq(configuration.SeqHost))
    .CreateLogger();

builder.Services
    .AddLogging(lb => lb.AddSerilog(Log.Logger, dispose: true))
    .AddSerilog(Log.Logger)
    .AddDiscordSocketClient()
    .AddLavaNode(nc =>
    {
        nc.Hostname = configuration.LavalinkHost;
        nc.Authorization = configuration.LavalinkPassword;
    })
    .AddMediatR(m => m
        .RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
    .AddSingleton<CommandService>()
    .AddSingleton(configuration)
    .AddHostedService<DiscordBotBackgroundService>();

using var app = builder.Build();

await app
    .RegisterDiscordSocketClientEvents()
    .RegisterLavaNodeEvents()
    .RunAsync();
