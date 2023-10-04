using Events.SendEmailEvents;
using MassTransit;

namespace EmailService.Consumers;

public class CancelSendEmailConsumer : IConsumer<ICancelSendEmailEvent>
{
    private readonly ILogger<CancelSendEmailConsumer> _logger;

    public CancelSendEmailConsumer(
        ILogger<CancelSendEmailConsumer> logger)
    {
        _logger = logger;
    }
    public Task Consume(ConsumeContext<ICancelSendEmailEvent> context)
    {
        throw new NotImplementedException();
    }
}