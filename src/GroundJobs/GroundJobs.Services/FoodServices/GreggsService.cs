using System.Text.RegularExpressions;
using GroundJobs.ServiceBus;
using GroundJobs.Services.Geocoding;

namespace GroundJobs.Services.FoodServices
{
    public class GreggsService : IService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var encodedPostcode = request.Command.Postcode.Replace(" ", "+");
            var html = HttpHelper.GetHtmlString($"https://www.greggs.co.uk/home/shop-finder/?Distance=5&Location={encodedPostcode}&action_go=Go&Position=&Bounds=&NotIn=");

            string marker = "<div id=\"store_results\">";
            string closeLocationTag = "</a>";
            string openLocationTag = "class=\"show_map_info\" href=\"#\">";
            string closeDistanceTag = "</div>";
            string openDistanceTag = "<div class=\"twelve columns locationDetails\">";

            var foundPos = html.IndexOf(marker);

            if (foundPos >= 0)
            {
                var location = html.SubstringBetweenStrings(openLocationTag, closeLocationTag, foundPos);
                var distanceText = html.SubstringBetweenStrings(openDistanceTag, closeDistanceTag, foundPos);
                var postcode = PostCodeService.GetLatLong(encodedPostcode);
                var value = Regex.Match(distanceText, "(GIR 0AA)|((([A-Z-[QVX]][0-9][0-9]?)|(([A-Z-[QVX]][A-Z-[IJZ]][0-9][0-9]?)|(([A-Z-[QVX]][0-9][A-HJKPSTUW])|([A-Z-[QVX]][A-Z-[IJZ]][0-9][ABEHMNPRVWXY])))) [0-9][A-Z-[CIKMOV]]{2})").Groups[2].Value;
                var distancePostcode = PostCodeService.GetLatLong(value);
                var distance = DistanceCalculator.GetDistance(postcode, distancePostcode);

                return new ClosestEateryResponse { Command = request.Command, LocationName = location, Distance = distance };
            }
            else
            {
                return default(ClosestEateryResponse);
            }
        }
    }
}