using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroundJobs.ServiceBus
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class ServiceBus
    {
        public static readonly ServiceBus Instance = new ServiceBus();

        public void Publish<T>(T command) where T : ICommand
        {
            var handlerTypes = GetTypesImplementing(typeof(ICommandHandler<T>));

            var handlers = handlerTypes.Select(t => (ICommandHandler<T>)Activator.CreateInstance(t));

            foreach (var handler in handlers)
            {
                handler.Execute(command);
            }
        }

        public TAggregation Aggregate<TServiceRequest, TServiceResponse, TAggregation>(TServiceRequest command) 
            where TServiceRequest : IServiceRequest<ICommand>
            where TServiceResponse : IServiceResponse<ICommand>
        {
            var aggregatorType = GetTypesImplementing(typeof (IServiceAggregation<TAggregation, TServiceResponse>)).Single();

            var aggregator = (IServiceAggregation<TAggregation, TServiceResponse>)Activator.CreateInstance(aggregatorType);

            var serviceTypes = GetTypesImplementing(typeof (IService<TServiceRequest, TServiceResponse>));

            var services = serviceTypes.Select(t => (IService<TServiceRequest, TServiceResponse>) Activator.CreateInstance(t));

            var serviceTasks = services.Select(s => Task.Factory.StartNew(() => s.Execute(command))).ToArray();

            Task.WaitAll(serviceTasks);

            return aggregator.Aggregate(serviceTasks.Select(t=> t.Result));
        }

        private IEnumerable<Type> GetTypesImplementing(Type type)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(x => x.GetTypes().Where(t => t.GetInterfaces().Contains(type)));
        } 
    }

    public interface ICommandHandler<in T> where T : ICommand
    {
        void Execute(T command);
    }

    public class CommandEventArgs : EventArgs
    {
        public ICommand Command;
    }

    public interface ICommand
    {
    }
}