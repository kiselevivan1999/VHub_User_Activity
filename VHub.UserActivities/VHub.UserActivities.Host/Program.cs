using Mapster;
using Microsoft.EntityFrameworkCore;
using RestEase.HttpClientFactory;
using Serilog;
using VHub.UserActivities.Application;
using VHub.UserActivities.Application.Movies.Consumers;
using VHub.UserActivities.Database.Configurations;
using VHub.UserActivities.Host;
using VHub.UserActivities.Host.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddHostedService<MovieCreatedConsumer>();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerService(builder.Configuration)
    .AddAuthenticationAndAuthorizationService(builder.Configuration)
    .AddAppServices(builder.Configuration)
    .AddScoped<JwtTokenHalper>();

builder.Services.AddRestEaseClient<WebApi.Contracts.IUserController>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("https://localhost:7203");
    }).ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = 
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        return handler;
    });;

builder.Services.AddDbContext<UserActivitiesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("vhub-ua-service-db-connection")));

builder.Services.AddSerilog(config => config.ReadFrom.Configuration(builder.Configuration));

var app = builder.Build();

TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
// app.UseKafkaBus(app.Lifetime);

app.Run();