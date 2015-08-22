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
            Assert.Throws<NotImplementedException>(() => ServiceBus.Publish(new TestCommand()));
        }
    }

    public class TestCommand : ICommand<TestSubject>
    {
        public void Execute(TestSubject data)
        {
            throw new NotImplementedException();
        }
    }

    public class TestSubject
    {
    }
}
