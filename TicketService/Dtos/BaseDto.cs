using Mapster;

namespace TicketService.Dtos;
//https://sd.blackball.lv/en/articles/read/18850
public abstract class BaseDto<TSource, TDestination> : IRegister
    where TSource : class, new()
    where TDestination : class, new()
{

    private TypeAdapterConfig Config { get; set; }
    
    public TDestination ToDestination(TSource source)
    {
        return source.Adapt<TDestination>();
    }
    
    public TDestination ToDestination()
    {
        return this.Adapt<TDestination>();
    }

    public static TSource ToSource(TDestination destination)
    {
        return destination.Adapt<TSource>();
    }
    protected virtual void AddCustomMappings() { }

    protected TypeAdapterSetter<TSource, TDestination> SetCustomMappings() => Config.ForType<TSource, TDestination>();

    protected TypeAdapterSetter<TDestination, TSource> SetCustomMappingsInverse() => Config.ForType<TDestination, TSource>();

    public void Register(TypeAdapterConfig config)
    {
        Config = config;
        AddCustomMappings();
    }
}