using System.Collections.Generic;

namespace GroundJobs.Services.FoodServices
{
    public class GoogleEateriesService : BaseHtmlScrapingService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public override ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var service = new GooglePlacesService();
            var nearest = service.Execute(new ClosestGooglePlaceRequest { Command = request.Command, Types = new List<GooglePlaceType> { GooglePlaceType.cafe, GooglePlaceType.bar, GooglePlaceType.restaurant } });
            return nearest != null 
                ? new ClosestEateryResponse { Command = request.Command, Distance = nearest.Distance, LocationName = nearest.Name } 
                : default (ClosestEateryResponse);
        }
    }
}