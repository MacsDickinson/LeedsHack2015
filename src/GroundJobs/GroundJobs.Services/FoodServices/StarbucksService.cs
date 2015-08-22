using GroundJobs.ServiceBus;
using GroundJobs.Services.Geocoding;

namespace GroundJobs.Services.FoodServices
{
    public class StarbucksService : IService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var postcode = PostCodeService.GetLatLong(request.Command.Postcode);
            var stores = HttpHelper.GetJson($"https://testhost.openapi.starbucks.com/location/v2/stores/nearest?latlng={postcode.Latitude},{postcode.Longitude}&format=json");

            return new ClosestEateryResponse
            {
                LocationName = $"Starbucks - {stores.store.name.ToString()}",
                Distance = float.Parse(stores.distance.ToString())
            };
        }
    }
}