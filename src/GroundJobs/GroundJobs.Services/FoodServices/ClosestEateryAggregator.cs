using System.Collections.Generic;
using System.Linq;
using GroundJobs.ServiceBus;

namespace GroundJobs.Services.FoodServices
{
    public class ClosestEateryAggregator : IServiceAggregation<EateryResult, ClosestEateryResponse>
    {
        public EateryResult Aggregate(IEnumerable<ClosestEateryResponse> responses)
        {
            var result = responses.Where(r=> r != null).OrderBy(r => r.Distance).First();
            return new EateryResult { Distance = result.Distance, LocationName = result.LocationName};
        }
    }

    public class ClosestEateriesAggregator : IServiceAggregation<IEnumerable<EateryResult>, ClosestEateryResponse>
    {
        public IEnumerable<EateryResult> Aggregate(IEnumerable<ClosestEateryResponse> responses)
        {
            var results = responses.Where(r => r != null).OrderBy(r => r.Distance);
            return results.Select(result => new EateryResult { Distance = result.Distance, LocationName = result.LocationName });
        }
    }
}