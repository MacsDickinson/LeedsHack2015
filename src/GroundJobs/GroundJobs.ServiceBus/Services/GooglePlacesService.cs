using System.Net.Http;
using System.Threading.Tasks;

namespace GroundJobs.ServiceBus.Services
{
    static class GoogleProvider
    {
        public class GoogleDetails
        {
            public string Location { get; set; }
            public float Distance { get; set; }
        }

        private static async Task<string> GetHTMLString(string url, HttpClient client)
        {
            var s = await client.GetStringAsync(url);

            return s;
        }

        public static async Task<GoogleDetails> GetNearest(string postcode)
        {
            var client = new HttpClient() { MaxResponseContentBufferSize = 1000000 };
            var encodedPostcode = postcode.Replace(" ", "%20");
            var output = await GetHTMLString($"http://www.Google.co.uk/en-gb/find-a-Google/{encodedPostcode}", client);

            return new GoogleDetails { Location = "test", Distance = float.Parse("123") };
        }
    }

    public class GetPlacesResponse : IServiceResponse<PostCodeSearchCommand>
    {
        public PostCodeSearchCommand Command { get; private set; }
        public float Distance;
        public string LocationName;
    }

    public class GooglePlacesService : IService<GetPlacesRequest, GetPlacesResponse>
    {
        public GetPlacesResponse Execute(GetPlacesRequest request)
        {
            var nearest = GoogleProvider.GetNearest(request.Command.Postcode);

            if (nearest == null)
                return null;

            return new GetPlacesResponse
            {
                Distance = nearest.Result.Distance,
                LocationName = nearest.Result.Location
            };
        }
    }

    public class GetPlacesRequest : IServiceRequest<PostCodeSearchCommand>
    {
        public PostCodeSearchCommand Command { get; set; }
    }
}