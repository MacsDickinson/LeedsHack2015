using System.Collections.Generic;
using System.Linq;
using GroundJobs.ServiceBus.Services;
using Xunit;

namespace GroundJobs.ServiceBus.Tests
{
    public class ServiceBusAggregationSpecification
    {
        [Fact]
        public void AggregateShouldCallServices()
        {
            var command = new TestAggregationCommand();
            var response = ServiceBus.Instance.Aggregate<TestAggregationRequest, TestAggregationResponse, TestAggregation>(new TestAggregationRequest {Command = command});
            Assert.Equal(2, response.Count);
        }
    }

    public class TestAggregationServiceAggregation : IServiceAggregation<TestAggregation, TestAggregationResponse>
    {
        public TestAggregation Aggregate(IEnumerable<TestAggregationResponse> responses)
        {
            return new TestAggregation {Count = responses.Sum(r => r.Command.One)};
        }
    }

    public class TestAggregationService : IService<TestAggregationRequest, TestAggregationResponse>
    {
        public TestAggregationResponse Execute(TestAggregationRequest request)
        {
            return new TestAggregationResponse { Command = request.Command};
        }
    }

    public class TestAggregationService2 : IService<TestAggregationRequest, TestAggregationResponse>
    {
        public TestAggregationResponse Execute(TestAggregationRequest request)
        {
            return new TestAggregationResponse { Command = request.Command };
        }
    }

    public class TestAggregationResponse : IServiceResponse<TestAggregationCommand>
    {
        public TestAggregationCommand Command { get; set; }
    }

    public class TestAggregationCommand : ICommand
    {
        public int One = 1;
    }

    public class TestAggregationRequest : IServiceRequest<TestAggregationCommand>
    {
        public TestAggregationCommand Command { get; set; }
    }

    public class TestAggregation
    {
        public int Count;
    }
}