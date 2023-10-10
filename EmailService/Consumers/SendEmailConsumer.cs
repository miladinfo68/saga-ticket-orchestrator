using Events.SendEmailEvents;
using MassTransit;
using System.Collections.Concurrent;

namespace EmailService.Consumers;

public class SendEmailConsumer : IConsumer<ISendEmailEvent>
{
    private readonly ILogger<SendEmailConsumer> _logger;

    public SendEmailConsumer(
        ILogger<SendEmailConsumer> logger)
    {
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<ISendEmailEvent> context)
    {
        var data = context.Message;
        if (data.Location == "London")
        {
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
        else
        {
            _logger.LogInformation($"message sent by email!");
        }
    }
}