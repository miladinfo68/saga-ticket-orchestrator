using Events.SendEmailEvents;
using Events.TicketEvents;
using MassTransit;

namespace SagaStateMachine;

public class TicketStateMachine : MassTransitStateMachine<TicketStateData>
{
    //states
    public State AddTicketSate { get; private set; }
    public State CancelTicketState { get; private set; }
    public State SendEmailTicketState { get; private set; }
    public State CancelSendEmailState { get; private set; }


    //events
    public Event<IAddTicketEvent> AddTicketEvent { get; private set; }
    public Event<ICancelGenerateTicketEvent> CancelGenerateTicketEvent { get; private set; }
    public Event<ISendEmailEvent> SendEmailEvent { get; private set; }
    public Event<ICancelSendEmailEvent> CancelSendEmailEvent { get; private set; }
   

    public TicketStateMachine()
    {
        InstanceState(s => s.CurrentState);

        Event(() => AddTicketEvent, e => e.CorrelateById(m => m.Message.TicketId));
        Event(() => CancelGenerateTicketEvent, e => e.CorrelateById(m => m.Message.TicketId));
        Event(() => SendEmailEvent, e => e.CorrelateById(m => m.Message.TicketId));
        Event(() => CancelSendEmailEvent, e => e.CorrelateById(m => m.Message.TicketId));

        //a message came from ticket service and initialized state by that
        Initially(
  When(AddTicketEvent)
                .Then(ctx =>
                {
                    ctx.Saga.TicketId = ctx.Message.TicketId;
                    ctx.Saga.TicketNumber = ctx.Message.TicketNumber;
                    ctx.Saga.Title = ctx.Message.Title;
                    ctx.Saga.Email = ctx.Message.Email;
                    ctx.Saga.Age = ctx.Message.Age;
                    ctx.Saga.Location = ctx.Message.Location;
                })
                .TransitionTo(AddTicketSate)
                .Publish(ctx => new GenerateTicketEvent(ctx.Saga))
        );

        //during AddTicketEvent some other events might occurred
        During(
            AddTicketSate,
  When(SendEmailEvent)
                .Then(ctx =>
                {

                    ctx.Saga.TicketId = ctx.Message.TicketId;
                    ctx.Saga.TicketNumber = ctx.Message.TicketNumber;
                    ctx.Saga.Title = ctx.Message.Title;
                    ctx.Saga.Email = ctx.Message.Email;
                    ctx.Saga.Age = ctx.Message.Age;
                    ctx.Saga.Location = ctx.Message.Location;

                })
                .TransitionTo(SendEmailTicketState)
        );

        During(
            AddTicketSate,
  When(CancelGenerateTicketEvent)
                .Then(ctx =>
                {

                    ctx.Saga.TicketId = ctx.Message.TicketId;
                    ctx.Saga.TicketNumber = ctx.Message.TicketNumber;
                    ctx.Saga.Title = ctx.Message.Title;
                    ctx.Saga.Email = ctx.Message.Email;
                    ctx.Saga.Age = ctx.Message.Age;
                    ctx.Saga.Location = ctx.Message.Location;

                })
                .TransitionTo(CancelTicketState)
        );


        //during SendEmailEvent some other events might occurred
        During(
            SendEmailTicketState,
            When(CancelSendEmailEvent)
                .Then(ctx =>
                {

                    ctx.Saga.TicketId = ctx.Message.TicketId;
                    ctx.Saga.TicketNumber = ctx.Message.TicketNumber;
                    ctx.Saga.Title = ctx.Message.Title;
                    ctx.Saga.Email = ctx.Message.Email;
                    ctx.Saga.Age = ctx.Message.Age;
                    ctx.Saga.Location = ctx.Message.Location;

                })
                .TransitionTo(CancelSendEmailState)
        );


    }
}