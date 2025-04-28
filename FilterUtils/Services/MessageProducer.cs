using MassTransit;

namespace WebApplication1;
public class MessageProducer
{
    private readonly IBus _bus;

    public MessageProducer(IBus bus)
    {
        _bus = bus;
    }

    public async Task SendMessageToQueue<T>(T message, string queueName) where T : class
    {
        var sendEndpoint = await _bus.GetSendEndpoint(new Uri($"queue:{queueName}"));
        await sendEndpoint.Send(message);
    }
    //builder.Services.AddScoped<SomeService>();
    //builder.Services.AddMassTransitServicesT<SomeConsumer>("localhost", "guest", "3688", "Myqueue");
}

