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
    public async Task Consume(ConsumeContext<ICancelSendEmailEvent> context)
    {
        var data = context.Message;

        await context.Publish<ICancelSendEmailEvent>(new
        {
            TicketId = data.TicketId,
            Title = data.Title,
            Email = data.Email,
            RequireDateTime = data.RequireDate,
            Age = data.Age,
            Loaction = data.Location,
        });
        _logger.LogInformation($"location is unavailable");
 
    }
}