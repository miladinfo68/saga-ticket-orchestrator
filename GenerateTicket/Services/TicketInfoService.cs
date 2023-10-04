using GenerateTicket.Common;
using GenerateTicket.Models;


namespace GenerateTicket.Services;

public class TicketInfoService:ITicketInfoService
{
    private readonly AppDbContext _ctx;

    public TicketInfoService(AppDbContext ctx)
    {
        _ctx = ctx;
    }
    public async Task<TicketInfo> AddTicketInfo(TicketInfo ticketInfo)
    {
        if (ticketInfo is null) return ticketInfo;
        ticketInfo.TicketNumber = StringGenerator.Generate();
        await _ctx.TicketInfos.AddAsync(ticketInfo);
        await _ctx.SaveChangesAsync();

        return ticketInfo;
    }

    public async Task<bool> RemoveTicketInfo(string ticketId)
    {
        var ticketInfo = await _ctx.TicketInfos.FindAsync(ticketId);
        if (ticketInfo is null) return false;
        _ctx.TicketInfos.Remove(ticketInfo);
        await _ctx.SaveChangesAsync();
        return true;
    }
}


