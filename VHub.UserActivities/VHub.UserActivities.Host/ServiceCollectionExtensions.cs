using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using KafkaFlow;

namespace VHub.UserActivities.Host;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationAndAuthorizationService(this IServiceCollection services,
    IConfiguration configuration)
    {
        string authorizationIdentityServerUri = configuration.GetValue<string>("IdentityUrlName")!;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            JwtBearerDefaults.AuthenticationScheme, conf =>
            {
                conf.Authority = authorizationIdentityServerUri;
                conf.Audience = authorizationIdentityServerUri;
                conf.BackchannelHttpHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                conf.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromSeconds(40),
                    ValidateAudience = false,
                };
            });

        services.AddAuthorization(conf =>
        {
            conf.AddPolicy("Admin", policy =>
            {
                policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireClaim(ClaimTypes.Role, "admin");
            });
        });

        return services;
    }

    public static IServiceCollection AddSwaggerService(this IServiceCollection services, IConfiguration configuration)
    {
        string authorizationIdentityServerUri = configuration.GetValue<string>("IdentityUrlName") + "connect/token";

        services.AddSwaggerGen(conf =>
        {
            conf.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "VHub.UserActivities.Host.xml"));
            conf.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    Password = new OpenApiOAuthFlow()
                    {
                        TokenUrl = new Uri(authorizationIdentityServerUri),
                        Scopes = new Dictionary<string, string>()
                        {
                            {"vhub", string.Empty}
                        }
                    },
                },
            });

            conf.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }
    
    public static IApplicationBuilder UseKafkaBus(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
    {
        var kafkaBus = app.ApplicationServices.CreateKafkaBus();

        lifetime.ApplicationStarted.Register(async () =>
        {
            try
            {
                await kafkaBus.StartAsync(lifetime.ApplicationStopping);
                Console.WriteLine("Kafka bus started successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to start Kafka bus: {ex.Message}");
            }
        });

        lifetime.ApplicationStopping.Register(() => { kafkaBus.StopAsync().GetAwaiter().GetResult(); });

        return app;
    }
}