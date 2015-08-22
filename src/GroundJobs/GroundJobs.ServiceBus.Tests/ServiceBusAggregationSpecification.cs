using Xunit;

namespace GroundJobs.ServiceBus.Tests
{
    public class ServiceBusAggregationSpecification
    {
        [Fact]
        public void AggregateShouldCallServices()
        {
            var testCommand = new TestCommand();
            var response = ServiceBus.Instance.Aggregate<GetEateriesRequest, GetEateriesResponse, GetEateriesAggregation>(new GetEateriesRequest());
        }
    }

    public class GetEateriesAggregation
    {
    }
}