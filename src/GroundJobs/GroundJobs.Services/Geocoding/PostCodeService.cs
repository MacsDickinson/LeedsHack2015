namespace GroundJobs.Services.Geocoding
{
    public static class PostCodeService
    {
        public static PostCode GetLatLong(string postcode)
        {
            var encodedPostcode = postcode.Replace(" ", string.Empty);
            var postcodeJson = HttpHelper.GetJson($"http://api.postcodes.io/postcodes/{encodedPostcode}");

            return new PostCode {Latitude = float.Parse(postcodeJson.result.latitude.ToString()), Longitude = float.Parse(postcodeJson.result.longitude.ToString())};
        }
    }

    public class PostCode
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}