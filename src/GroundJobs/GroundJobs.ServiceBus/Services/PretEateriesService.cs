using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GroundJobs.ServiceBus
{
    public static class StringExtensions
    {
        public static string SubstringBetweenStrings(this string value, string start, string end, int startPos)
        {
            var returnStartPos = value.IndexOf(start, startPos) + start.Length;
            var returnEndPos = value.IndexOf(end, returnStartPos);
            var retval = value.Substring(returnStartPos, returnEndPos - returnStartPos);

            return retval;
        }
    }

    static class PretProvider
    {
        public class PretDetails
        {
            public string Location { get; set; }
            public float Distance { get; set; }
        }

        private static async Task<string> GetHTMLString(string url, HttpClient client)
        {
            var s = await client.GetStringAsync(url);

            return s;
        }

        public static async Task<PretDetails> GetNearest(string postcode)
        {
            var client = new HttpClient() { MaxResponseContentBufferSize = 1000000 };
            var encodedPostcode = postcode.Replace(" ", "%20");
            var output = await GetHTMLString($"http://www.pret.co.uk/en-gb/find-a-pret/{encodedPostcode}", client);

            string marker = "<div class=\"panel-heading\">";
            string closeLocationTag = "</h3>";
            string openLocationTag = "<h3>";
            string closeDistanceTag = " Km away</span>";
            string openDistanceTag = "<span class=\"destination\">Approximately ";

            var foundPos = output.IndexOf(marker);

            if (foundPos >= 0)
            {
                var location = output.SubstringBetweenStrings(openLocationTag, closeLocationTag, foundPos);
                var distance = output.SubstringBetweenStrings(openDistanceTag, closeDistanceTag, foundPos);

                return new PretDetails { Location = location, Distance = float.Parse(distance) };
            }
            else
            {
                return null;
            }
        }
    }

    public class GetEateriesCommand : ICommand
    {
        public string Postcode;
    }

    public class GetEateriesResponse : IServiceResponse<GetEateriesCommand>
    {
        public GetEateriesCommand Command { get; private set; }
        public float Distance;
        public string LocationName;
    }

    public class PretEateriesService : IService<GetEateriesRequest, GetEateriesResponse>
    {
        public GetEateriesResponse Execute(GetEateriesRequest request)
        {
            var nearest = PretProvider.GetNearest(request.Command.Postcode);

            if (nearest == null)
                return null;

            return new GetEateriesResponse
            {
                Distance = nearest.Result.Distance,
                LocationName = nearest.Result.Location
            };
        }
    }

    public class GetEateriesRequest : IServiceRequest<GetEateriesCommand>
    {
        public GetEateriesCommand Command { get; set; }
    }
}