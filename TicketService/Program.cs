using MassTransit;
using MassTransit.MultiBus;
using MessageBrockers;
using Microsoft.EntityFrameworkCore;
using TicketService.Consumers;
using TicketService.Mapper;
using TicketService.Models;
using TicketService.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(defaultConnection));
//builder.Services.addAuoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddMapster();

builder.Services.AddScoped<ITicketService, TicketService.Services.TicketService>();

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<GetAddedTicketConsumer>();
    config.AddConsumer<TicketGenerateCanceledConsumer>();

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
            ep.ConfigureConsumer<GetAddedTicketConsumer>(context);
            ep.ConfigureConsumer<TicketGenerateCanceledConsumer>(context);
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