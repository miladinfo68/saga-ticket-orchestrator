using Events.TicketEvents;
using MassTransit;
using TicketService.Services;

namespace TicketService.Consumers;

public class TicketGenerateCanceledConsumer : IConsumer<ICancelGenerateTicketEvent>
{
    private readonly ILogger<TicketGenerateCanceledConsumer> _logger;
    private readonly ITicketService _ticketService;

    public TicketGenerateCanceledConsumer(
        ILogger<TicketGenerateCanceledConsumer> logger,
        ITicketService ticketService)
    {
        _logger = logger;
        _ticketService = ticketService;
    }
    public async Task Consume(ConsumeContext<ICancelGenerateTicketEvent> context)
    {
        var data = context.Message;
        var res = await _ticketService.DeleteTicket(data.TicketId.ToString());
        _logger.LogInformation(res ? $"Ticket {data.TicketId} removed" : $"Ticket {data.TicketId} removed failed");

    }
}

