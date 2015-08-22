using System;

namespace GroundJobs.ServiceBus
{
    /// <summary>
    /// This ServiceBus will (eventually) execute commands async
    /// Do the following to use it:
    ///     Create a "Command" by implementing ICommand<T>
    ///     Execute the command by using ServiceBus.Instance.Publish(command);
    /// </summary>
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
