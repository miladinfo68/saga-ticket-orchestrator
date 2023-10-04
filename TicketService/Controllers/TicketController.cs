using Events.TicketEvents;
using Microsoft.AspNetCore.Mvc;
using TicketService.Dtos;
using TicketService.Models;
using TicketService.Services;
using Mapster;
using MassTransit;

namespace TicketService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly IBus _bus;

    public TicketController(ITicketService ticketService, IBus bus)
    {
        _ticketService = ticketService;
        _bus = bus;
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddTicketDto dto)
    {
        var ticket = dto.Adapt<Ticket>();
        var _ = await _ticketService.AddTicket(ticket);
        var addedTicketResponse = ticket.Adapt<ResponseTicketDto>();
        
        if (addedTicketResponse?.TicketId is null) 
            return StatusCode(StatusCodes.Status400BadRequest);
        
        await SendMessageToBus(addedTicketResponse);
        return StatusCode(StatusCodes.Status201Created);

    }

    private async Task SendMessageToBus(ResponseTicketDto dto)
    {
        var endPoint = await _bus.GetSendEndpoint(new Uri($"queue:{MessageBrockers.RabbitMqQueues.SagaQueue}"));
        await endPoint.Send<IGetAddedTicketEvent>(new
        {
            TicketId = Guid.Parse(dto.TicketId),
            Title = dto.Title,
            Email = dto.Email,
            RequireDateTime = dto.RequireDate,
            Age = dto.Age,
            Loaction = dto.Location
        });
    }
}