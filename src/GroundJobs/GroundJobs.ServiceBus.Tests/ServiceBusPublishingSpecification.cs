using System;
using Xunit;

namespace GroundJobs.ServiceBus.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class ServiceBusPublishingSpecification
    {
        
        public ServiceBusPublishingSpecification()
        {
        }

        [Fact]
        public void PublishShouldAcceptAnICommand()
        {
            var testCommand = new TestCommand();
            Assert.Null(testCommand.Data);
            ServiceBus.Instance.Publish(testCommand);
            Assert.NotNull(testCommand.Data);
            Assert.Equal("Hello world!", testCommand.Data.Speak);
        }

        [Fact]
        public void PublishShouldRaiseOnCompletedEvent()
        {
            var testCommand = new TestCommand();
            var pass = false;
            ServiceBus.Instance.OnCommandComplete += (sender,handler) => pass = true;
            ServiceBus.Instance.Publish(testCommand);
            Assert.True(pass);
        }

        [Fact]
        public void PublishShouldMoveThroughMultipleCommandHandlers()
        {
            Counter.Count = 0;
            var testCommand = new MultipleHandlersTestCommand();
            ServiceBus.Instance.Publish(testCommand);
            Assert.Equal(2, Counter.Count);
        }

        private void Instance_CommandComplete(object sender, CommandEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public void Execute(TestCommand command)
        {
            command.Data = new TestSubject { Speak = "Hello world!" };
        }
    }

    public class TestCommand : ICommand
    {
        public TestSubject Data { get; set; }
    }

    public class TestSubject
    {
        public string Speak { get; set; }
    }

    public class MultipleHandlersTestCommand : ICommand
    {
    }

    public static class Counter
    {
        public static int Count = 0;
    }

    public class TestCommandHandler1 : ICommandHandler<MultipleHandlersTestCommand>
    {
        public void Execute(MultipleHandlersTestCommand command)
        {
            Counter.Count++;
        }
    }

    public class TestCommandHandler2 : ICommandHandler<MultipleHandlersTestCommand>
    {
        public void Execute(MultipleHandlersTestCommand command)
        {
            Counter.Count++;
        }
    }
}
