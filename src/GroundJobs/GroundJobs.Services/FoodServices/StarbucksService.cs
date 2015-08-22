namespace GroundJobs.Services.FoodServices
{
    public class StarbucksService : BaseHtmlScrapingService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public override ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            
            var encodedPostcode = request.Command.Postcode.Replace(" ", string.Empty);
            var postcodeData = GetHTMLString($"http://api.postcodes.io/postcodes/{encodedPostcode}");
            postcodeData.Wait();
            var postcode = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(postcodeData.Result);

            var latitude = postcode.result.latitude.ToString();
            var longitude = postcode.result.longitude.ToString();
            var storesData = GetHTMLString($"https://testhost.openapi.starbucks.com/location/v2/stores/nearest?latlng={latitude},{longitude}&format=json");
            storesData.Wait();
            var stores = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(storesData.Result);

            return new ClosestEateryResponse
            {
                LocationName = stores.store.name.ToString(),
                Distance = float.Parse(stores.distance.ToString())
            };
        }
    }
}