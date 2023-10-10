using EmailService.Consumers;
using MassTransit;
using MessageBrockers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<SendEmailConsumer>();
    config.AddConsumer<CancelSendEmailConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(RabbitMqConfigs.RabbitHostName, RabbitMqConfigs.VirtualHost, hostCfg =>
        {
            hostCfg.Username(RabbitMqConfigs.UserName);
            hostCfg.Password(RabbitMqConfigs.Password);
        });
        
        cfg.ReceiveEndpoint(RabbitMqQueues.SagaQueue, ep =>
        {
            ep.PrefetchCount = 10;
            ep.ConfigureConsumer<SendEmailConsumer>(context);
            ep.ConfigureConsumer<CancelSendEmailConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
