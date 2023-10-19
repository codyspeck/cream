using Cream.Api;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddWebServices(builder.Configuration);

var app = builder.Build();

app.Run();
