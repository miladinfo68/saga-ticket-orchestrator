using GenerateTicket.Models;

namespace GenerateTicket.Services;

public interface ITicketInfoService
{
     Task<TicketInfo> AddTicketInfo(TicketInfo ticketInfo);
     Task<bool> RemoveTicketInfo(string ticketId);
}