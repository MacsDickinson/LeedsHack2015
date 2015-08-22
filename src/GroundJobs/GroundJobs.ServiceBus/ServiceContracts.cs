namespace GroundJobs.ServiceBus
{
    public interface IServiceRequest<T> where T : ICommand
    {
        T Command { get; }
    }

    public interface IServiceResponse<T> where T : ICommand
    {
        T Command { get; }
    }

    public interface IService<TCommand>
        where TCommand : ICommand
    {
        IServiceResponse<TCommand> Execute(IServiceRequest<TCommand> request);
    }
}