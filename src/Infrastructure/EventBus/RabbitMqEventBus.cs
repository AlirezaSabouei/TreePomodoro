/*using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Events;


namespace Yooresh.Infrastructure.EventBus;

public class RabbitMqEventBus : IEventBus,IDisposable
{
    private readonly IConnection _connection;
    private readonly RabbitMQ.Client.IModel _channel;
    private readonly Dictionary<string, List<IIntegrationEventHandler<IntegrationEvent>>> _subscribers;

    public RabbitMqEventBus(string rabbitMqHost, string rabbitMqUserName, string rabbitMqPassword)
    {
        IConnectionFactory connectionFactory = new ConnectionFactory
        {
            HostName = rabbitMqHost,
            UserName = rabbitMqUserName,
            Password = rabbitMqPassword
        };
        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _subscribers = new Dictionary<string, List<IIntegrationEventHandler<IntegrationEvent>>>();
    }

    public void Publish<T>(T message) where T : IntegrationEvent
    {
        var eventName = message.GetType().Name;

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

        _channel.BasicPublish(exchange: eventName, routingKey: eventName, body: body);
    }

    public void Subscribe<TEvent, TEventHandler>(TEventHandler eventHandler) where TEvent : IntegrationEvent where TEventHandler : IIntegrationEventHandler<IntegrationEvent>
    {
        throw new NotImplementedException();
    }

    public void Subscribe<T, THandler>() where T : IntegrationEvent where THandler : IIntegrationEventHandler<T>
    {
        var eventName = typeof(T).Name;
        var handlerType = typeof(THandler);

        if (!_subscribers.ContainsKey(eventName))
        {
            _subscribers.Add(eventName, new List<IIntegrationEventHandler<T>>());
        }

        if (_subscribers[eventName].All(s => s.HandlerType != handlerType))
        {
            _subscribers[eventName].Add(new EventSubscription(handlerType));
        }

        _channel.QueueBind(queue: eventName, exchange: eventName, routingKey: eventName);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var message = Encoding.UTF8.GetString(ea.Body);
            await ProcessEvent(eventName, message);
        };

        _channel.BasicConsume(queue: eventName, autoAck: true, consumer: consumer);
    }

    public void StartConsuming()
    {
        foreach (var eventName in _subscribers.Keys)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var eventType = _subscribers.Keys.FirstOrDefault(key => key == ea.RoutingKey); 
                var eventHandlers = _subscribers[eventType];

                foreach (var eventHandler in eventHandlers)
                {
                    eventHandler.Handle(JsonConvert.DeserializeObject<IntegrationEvent>(message));
                }
            };

            _channel.BasicConsume(queue: eventName, autoAck: true, consumer: consumer);
        }
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}*/