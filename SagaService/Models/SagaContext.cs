using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace SagaService.Models;

public class SagaTicketContext : SagaDbContext
{
    public SagaTicketContext(DbContextOptions options) : base(options)
    {
    }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new TicketStateMap(); }
    }
}