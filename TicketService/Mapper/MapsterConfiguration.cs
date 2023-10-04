using System.Reflection;
using Mapster;
using TicketService.Dtos;

namespace TicketService.Mapper;

public static class MapsterConfiguration
{
    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        var applicationAssembly = typeof(BaseDto<,>).Assembly;
        typeAdapterConfig.Scan(applicationAssembly);
        
        return services;
    }
}