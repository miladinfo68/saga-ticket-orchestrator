using TicketService.Models;

namespace TicketService.Services;

public interface ITicketService
{
    Task<Ticket> AddTicket(Ticket ticket);
    Task<bool> DeleteTicket(string ticketId);
}