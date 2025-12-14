using Confluent.Kafka;
using KafkaFlow;
using VHub.UserActivities.Application.Contracts.Events;

namespace VHub.UserActivities.Application.Movies.Producers;

public class UsersNotificationRequestedProducer(
    IMessageProducer<UsersNotificationRequestedProducer> producer)
    : IUsersNotificationRequestedProducer
{
    private readonly IMessageProducer<UsersNotificationRequestedProducer> _producer =
        producer ?? throw new ArgumentNullException(nameof(producer));
    
    public async Task SendUsersNotificationRequestedEvent(
        UsersNotificationRequestedEvent usersNotificationRequestedEvent, CancellationToken cancellationToken)
    {
        try
        {
            _ = await _producer.ProduceAsync(messageKey: null, usersNotificationRequestedEvent);
        }
        catch (ProduceException<Null, string> e)
        {
            Console.WriteLine($"Ошибка отправки события: {e.Error.Reason}");
            throw;
        }
    }
}