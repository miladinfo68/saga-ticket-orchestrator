using System.ComponentModel.DataAnnotations;
using TicketService.Models;

namespace TicketService.Dtos;

public class AddTicketDto //: BaseDto<AddTicketDto, Ticket>
{
    public string TicketId { get; set; } = Guid.NewGuid().ToString();
    public string TicketNumber { get; set; }
    public string Title { get; set; }
    [Required] public string Email { get; set; }
    public DateTime RequireDate { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public int Age { get; set; }
    public string Location { get; set; }
}

public class ResponseTicketDto //: BaseDto<Ticket,ResponseTicketDto >
{
    public string TicketId { get; set; }
    public string TicketNumber { get; set; }
    public string Title { get; set; }
    public string Email { get; set; }
    public DateTime RequireDate { get; set; }
    public DateTime CreateDate { get; set; } 
    public int Age { get; set; }
    public string Location { get; set; }
}