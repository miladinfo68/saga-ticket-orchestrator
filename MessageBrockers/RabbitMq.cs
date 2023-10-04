using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBrockers;

public class RabbitMq
{
    public static IBusControl ConfigureBus(
        IServiceProvider serviceProvider , 
        Action<IRabbitMqBusFactoryConfigurator ,IRabbitMqHost>? action=null
    )
    {
        return Bus.Factory.CreateUsingRabbitMq(config =>
        {
            config.Host(new Uri(RabbitMqConfigs.RabbitMqUri), hostConfig =>
            {
                hostConfig.Username(RabbitMqConfigs.UserName);
                hostConfig.Password(RabbitMqConfigs.Password);
            });
            
            config.ConfigureEndpoints(serviceProvider.GetRequiredService<IBusRegistrationContext>());
        });
    }
}