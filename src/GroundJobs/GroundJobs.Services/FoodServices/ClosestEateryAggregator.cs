using System.Collections.Generic;
using System.Linq;
using GroundJobs.ServiceBus;

namespace GroundJobs.Services.FoodServices
{
    public class ClosestEateryAggregator : IServiceAggregation<EateryResult, ClosestEateryResponse>
    {
        public EateryResult Aggregate(IEnumerable<ClosestEateryResponse> responses)
        {
            var result = responses.OrderBy(r => r.Distance).First();
            return new EateryResult { Distance = result.Distance, LocationName = result.LocationName};
        }
    }
}