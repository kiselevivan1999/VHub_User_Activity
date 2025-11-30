using System.Reflection;
using DbUp;

namespace VHub.UserActivities.Database.Migrations;

public static class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();
		
        var connection = configuration.GetConnectionString("vhub-ua-service-db-connection");
        if (connection == null)
        {
            throw new Exception("Строка подключения к БД сервиса VHub.UserActivities.Host не найдена в конфигурации.");
        }

        var builder = DeployChanges.To.
            PostgresqlDatabase(connection)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        var result = builder.PerformUpgrade();

        if (!result.Successful)
        {
            throw new Exception("Ошибка наката миграций БД сервиса VHub.UserActivities.Host.", result.Error);
        }
		
        Console.WriteLine("Миграции БД сервиса VHub.UserActivities.Host завершены успешно, приложение готово к работе.");
    }
}