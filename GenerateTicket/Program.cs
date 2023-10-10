using GenerateTicket.Consumers;
using GenerateTicket.Models;
using GenerateTicket.Services;
using MassTransit;
using MessageBrockers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(defaultConnection));

builder.Services.AddScoped<ITicketInfoService, TicketInfoService>();


builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<GenerateTicketConsumer>();
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
            ep.ConfigureConsumer<GenerateTicketConsumer>(context);
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
