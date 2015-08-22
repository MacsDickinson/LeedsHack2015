using System;
using Xunit;

namespace GroundJobs.ServiceBus.Tests
{
    public class ServiceBusAggregationSpecification
    {
        [Fact]
        public void AggregateShouldCallServices()
        {
            var testCommand = new TestCommand();

        }
    }
    
    public class ServiceBusPublishingSpecification
    {
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
        public void PublishShouldMoveThroughMultipleCommandHandlers()
        {
            Counter.Count = 0;
            var testCommand = new MultipleHandlersTestCommand();
            ServiceBus.Instance.Publish(testCommand);
            Assert.Equal(2, Counter.Count);
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
