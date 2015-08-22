namespace GroundJobs.Services.FoodServices
{
    public class GoogleEateriesService : BaseHtmlScrapingService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public override ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var service = new GooglePlacesService();
            var nearest = service.Execute(new ClosestGooglePlaceRequest { Command = request.Command, Types = { EateryType.cafe, EateryType.bar, EateryType.restaurant } });
            return nearest != null 
                ? new ClosestEateryResponse { Command = request.Command, Distance = nearest.Distance, LocationName = nearest.LocationName } 
                : default (ClosestEateryResponse);
        }
    }
}