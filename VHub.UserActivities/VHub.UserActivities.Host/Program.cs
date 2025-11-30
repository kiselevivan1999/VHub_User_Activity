using Mapster;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VHub.UserActivities.Application;
using VHub.UserActivities.Database.Configurations;
using VHub.UserActivities.Host;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddAppServices(builder.Configuration);

builder.Services.AddDbContext<UserActivitiesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("vhub-ua-service-db-connection")));

builder.Services.AddSerilog(config => config.ReadFrom.Configuration(builder.Configuration));

var app = builder.Build();
app.UseMiddleware<SimpleJwtMiddleware>();

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
app.UseKafkaBus(app.Lifetime);

app.Run();