using System.Collections;
using System.Collections.Generic;

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

    public interface IService<in TRequest, out TResponse>
        where TRequest : IServiceRequest<ICommand>
        where TResponse : IServiceResponse<ICommand>
    {
        TResponse Execute(TRequest request);
    }

    public interface IServiceAggregation<out TAggregate, in TServiceResponse>
        where TServiceResponse : IServiceResponse<ICommand>
    {
        TAggregate Aggregate(IEnumerable<TServiceResponse> responses);
    }
}