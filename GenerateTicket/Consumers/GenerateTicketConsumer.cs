using Events.SendEmailEvents;
using Events.TicketEvents;
using GenerateTicket.Models;
using GenerateTicket.Services;
using Mapster;
using MassTransit;

namespace GenerateTicket.Consumers;

public class GenerateTicketConsumer : IConsumer<IGenerateTicketEvent>
{
    private readonly ILogger<GenerateTicketConsumer> _logger;
    private readonly ITicketInfoService _ticketInfoService;

    public GenerateTicketConsumer(
        ITicketInfoService ticketInfoService,
        ILogger<GenerateTicketConsumer> logger)
    {
        _ticketInfoService = ticketInfoService;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<IGenerateTicketEvent> context)
    {
        var data = context.Message;

        if (data?.TicketId is null || data?.TicketId == Guid.Empty) 
            await Task.CompletedTask;


        var ticketInfo = data.Adapt<TicketInfo>();
        if (data.Age < 80)
        {
            var res = await _ticketInfoService.AddTicketInfo(ticketInfo);

            await context.Publish<ISendEmailEvent>(new
            {
                TicketId = data.TicketId,
                Title = data.Title,
                Email = data.Email,
                RequireDateTime = data.RequireDate,
                Age = data.Age,
                Loaction = data.Location,
                TicketNumber = res.TicketNumber

            });


            _logger.LogInformation($"TicketId {ticketInfo.TicketId} sent!");

        }
        else
        {
            await context.Publish<ICancelGenerateTicketEvent>(new
            {
                TicketId = data.TicketId,
                Title = data.Title,
                Email = data.Email,
                RequireDateTime = data.RequireDate,
                Age = data.Age,
                Loaction = data.Location

            });
            _logger.LogInformation($"Sending TicketId {ticketInfo.TicketId} canceled!");
        }


    }
}