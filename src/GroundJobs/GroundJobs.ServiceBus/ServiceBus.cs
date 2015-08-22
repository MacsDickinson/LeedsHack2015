using System;

namespace GroundJobs.ServiceBus
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class ServiceBus
    {
        public static readonly ServiceBus Instance = new ServiceBus();

        public static void Publish<T>(ICommand<T> command)
        {
            CommandComplete(Instance, new CommandEventArgs { Command = command});
        }

        public delegate void OnCommandComplete(CommandEventArgs e);
        public static event EventHandler<CommandEventArgs> CommandComplete;
    }

    public class CommandEventArgs : EventArgs
    {
        public object Command;
    }

    public interface ICommand<in T>
    {
        void Execute(T data);
    }
}
