using KafkaFlow;

namespace VHub.UserActivities.Host;

public static class ServiceCollectionExtensions
{
    public static IApplicationBuilder UseKafkaBus(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
    {
        var kafkaBus = app.ApplicationServices.CreateKafkaBus();
    
        lifetime.ApplicationStarted.Register(async () =>
        {
            try
            {
                await kafkaBus.StartAsync(lifetime.ApplicationStopping);
                Console.WriteLine("Kafka bus started successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to start Kafka bus: {ex.Message}.");
            }
        });

        lifetime.ApplicationStopping.Register(() =>
        {
            kafkaBus.StopAsync().GetAwaiter().GetResult();
        });

        return app;
    }
}