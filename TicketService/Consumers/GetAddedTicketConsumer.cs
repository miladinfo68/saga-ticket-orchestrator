using Events.TicketEvents;
using MassTransit;

namespace TicketService.Consumers;

public class GetAddedTicketConsumer:IConsumer<IGetAddedTicketEvent>
{
    private readonly ILogger<GetAddedTicketConsumer> _logger;

    public GetAddedTicketConsumer(ILogger<GetAddedTicketConsumer> logger)
    {
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<IGetAddedTicketEvent> context)
    {
        var data = context.Message;
        await context.Publish<IAddTicketEvent>(new
        {
            TicketId = data.TicketId,
            Title = data.Title,
            Email = data.Email,
            RequireDateTime = data.RequireDate,
            Age = data.Age,
            Loaction = data.Location
                
        });
        _logger.LogInformation("a messages has been received!");

     
    }
}