using System;
using System.Collections.Generic;
using System.Linq;

namespace GroundJobs.ServiceBus
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class ServiceBus
    {
        public static readonly ServiceBus Instance = new ServiceBus();

        public void Publish<T>(T command) where T : ICommand
        {
            var handlerTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICommandHandler<T>))));

            var handlers = handlerTypes.Select(t => (ICommandHandler<T>)Activator.CreateInstance(t));

            foreach (var handler in handlers)
            {
                handler.Execute(command);
            }

            OnCommandComplete?.Invoke(Instance, new CommandEventArgs { Command = command });
        }

        public delegate void CommandComplete(CommandEventArgs e);
        public event EventHandler<CommandEventArgs> OnCommandComplete;
    }

    public interface ICommandHandler<in T> where T : ICommand
    {
        void Execute(T command);
    }

    public class CommandEventArgs : EventArgs
    {
        public object Command;
    }

    public interface ICommand
    {
    }
}
