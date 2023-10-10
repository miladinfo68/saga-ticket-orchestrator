using Events.SendEmailEvents;
using Events.TicketEvents;
using GenerateTicket.Services;
using MassTransit;

namespace GenerateTicket.Consumers;

public class CancelSendEmailConsumer : IConsumer<ICancelSendEmailEvent>
{
    private readonly ILogger<CancelSendEmailConsumer> _logger;

    private readonly ITicketInfoService _ticketInfoService;

    public CancelSendEmailConsumer(ITicketInfoService ticketInfoService,
        ILogger<CancelSendEmailConsumer> logger)
    {
        _ticketInfoService = ticketInfoService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ICancelSendEmailEvent> context)
    {
        var data = context.Message;
        var res = await _ticketInfoService.RemoveTicketInfo(data.TicketId.ToString());
        await context.Publish<ICancelGenerateTicketEvent>(new
        {
            TicketId = data.TicketId,
            Title = data.Title,
            Email = data.Email,
            RequireDateTime = data.RequireDate,
            Age = data.Age,
            Loaction = data.Location,
        }); 
        _logger.LogInformation($"the message has been sent to ICancelGenerateTicketEvent in the TicketService");
    }
}