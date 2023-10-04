using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SagaStateMachine;

namespace SagaService.Models;

public class TicketStateMap : SagaClassMap<TicketStateData>
{
    protected override void Configure(EntityTypeBuilder<TicketStateData> entity, ModelBuilder model)
    {
        entity.Property(x => x.CurrentState).HasMaxLength(64);
        entity.Property(x => x.TicketCreatedDate);
    }
}