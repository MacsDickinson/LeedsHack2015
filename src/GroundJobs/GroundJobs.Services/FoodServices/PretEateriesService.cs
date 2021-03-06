﻿using System.Threading.Tasks;

namespace GroundJobs.Services.FoodServices
{
    public class PretEateriesService : BaseHtmlScrapingService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public static async Task<Details> GetNearest(string postcode)
        {
            var encodedPostcode = postcode.Replace(" ", "%20");
            var output = await GetHTMLString($"http://www.pret.co.uk/en-gb/find-a-pret/{encodedPostcode}");

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

                return new Details { Location = $"Pret-a-Manger - {location}", Distance = float.Parse(distance) };
            }
            else
            {
                return null;
            }
        }

        public override ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var nearest = GetNearest(request.Command.Postcode);

            if (nearest == null)
                return null;

            return new ClosestEateryResponse
            {
                Distance = nearest.Result.Distance,
                LocationName = nearest.Result.Location
            };
        }
    }
}