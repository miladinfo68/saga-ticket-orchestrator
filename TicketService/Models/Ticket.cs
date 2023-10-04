﻿namespace TicketService.Models;

public class Ticket
{
    public string TicketId { get; set; }
    public string TicketNumber { get; set; }
    public string Title { get; set; }
    public string Email { get; set; }
    public DateTime RequireDateTime { get; set; }
    public DateTime CreateDate { get; set; }
    public int Age { get; set; }
    public string Location { get; set; }
}