using TicketService.Models;

namespace TicketService.Services;

public class TicketService : ITicketService
{
    private readonly AppDbContext _ctx;

    public TicketService(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Ticket> AddTicket(Ticket ticket)
    {
        if (ticket is null) return ticket;
        await _ctx.Tickets.AddAsync(ticket);
        await _ctx.SaveChangesAsync();

        return ticket;
    }

    public async Task<bool> DeleteTicket(string ticketId)
    {
        var ticket = await _ctx.Tickets.FindAsync(ticketId);
        if (ticket is null) return false;
        _ctx.Tickets.Remove(ticket);
        await _ctx.SaveChangesAsync();
        return true;
    }
}