using Api.IoC;
using Infrastructure.IoC;
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.ConfigureApiServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

var app = builder.Build();
app.UseApiServices();
await app.RunAsync();
