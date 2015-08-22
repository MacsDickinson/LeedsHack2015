using GroundJobs.ServiceBus;
using GroundJobs.Services.Geocoding;

namespace GroundJobs.Services.FoodServices
{
    public class McDonaldsService : IService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var postcode = PostCodeService.GetLatLong(request.Command.Postcode);
            var stores = HttpHelper.GetJson($"http://www2.mcdonalds.co.uk/googleapps/GoogleSearchAction.do?method=searchLocation&searchTxtLatlng=({postcode.Latitude}%2C%20{postcode.Longitude})&actionType=filterRestaurant&&language=en&country=uk");

            return new ClosestEateryResponse
            {
                LocationName = stores.results[0].name.ToString(),
                Distance = DistanceCalculator.GetDistance(postcode.Latitude, postcode.Longitude, float.Parse(stores.results[0].latitude.ToString()), float.Parse(stores.results[0].longitude.ToString()))
            };
        }
    }
}