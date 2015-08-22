namespace GroundJobs.ServiceBus
{
    public interface IServiceRequest<out T> where T : ICommand
    {
        T Command { get; }
    }

    public interface IServiceResponse<out T> where T : ICommand
    {
        T Command { get; }
    }

    public interface IService<T, in TRequest, out TResponse> 
        where T : ICommand
        where TRequest : IServiceRequest<T>
        where TResponse : IServiceResponse<T>
    {
        TResponse Execute(TRequest request);
    }

    public interface IServiceAggregation<T> where T : ICommand
    {
        
    }
}