namespace GroundJobs.Services.FoodServices
{
    public class GooglePlacesService : BaseHtmlScrapingService<ClosestGooglePlaceRequest, ClosestGooglePlaceResponse>
    {
        public override ClosestGooglePlaceResponse Execute(ClosestGooglePlaceRequest request)
        {
            var encodedPostcode = request.Command.Postcode.Replace(" ", string.Empty);
            var postcodeData = GetHTMLString($"http://api.postcodes.io/postcodes/{encodedPostcode}");
            postcodeData.Wait();
            var postcode = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(postcodeData.Result);

            float latitude = float.Parse(postcode.result.latitude.ToString());
            float longitude = float.Parse(postcode.result.longitude.ToString());
            const string googleAPIKey = "AIzaSyCM_pHgWShJwu4sYj_M79lDQ6Tpw9zV_9k";
            var storesData = GetHTMLString($"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={latitude},{longitude}&radius=500&types={request.Type}&name={request.Name}&key={googleAPIKey}");
            storesData.Wait();
            var googlePlaces = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(storesData.Result);

            if (googlePlaces.results.Count <= 0) return default(ClosestGooglePlaceResponse);

            var closestPlace = new ClosestGooglePlaceResponse
            {
                LocationName = googlePlaces.results[0].name.ToString(),
                Latitude = float.Parse(googlePlaces.results[0].geometry.location.lat.ToString()),
                Longitude = float.Parse(googlePlaces.results[0].geometry.location.lng.ToString()),
            };
            closestPlace.SetDistanceFrom(latitude, longitude);
            return closestPlace;
        }
    }
}