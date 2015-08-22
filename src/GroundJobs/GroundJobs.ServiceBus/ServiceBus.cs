using System;

namespace GroundJobs.ServiceBus
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class ServiceBus
    {
        public static readonly ServiceBus Instance = new ServiceBus();

        public void Publish<T>(ICommand<T> command)
        {
            command.Execute();

            OnCommandComplete(Instance, new CommandEventArgs { Command = command});
        }

        public delegate void CommandComplete(CommandEventArgs e);
        public event EventHandler<CommandEventArgs> OnCommandComplete;
    }

    public class CommandEventArgs : EventArgs
    {
        public object Command;
    }

    public interface ICommand<T>
    {
        T Data { get; set; }
        void Execute();
    }
}
