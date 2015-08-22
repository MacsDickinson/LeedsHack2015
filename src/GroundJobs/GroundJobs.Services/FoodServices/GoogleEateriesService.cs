using System;
using System.Linq;

namespace GroundJobs.Services.FoodServices
{
    public class GoogleCafesService : BaseHtmlScrapingService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public override ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var service = new GooglePlacesService();
            var nearest = service.Execute(new ClosestGooglePlaceRequest { Command = request.Command, Type = EateryType.cafe });
            return new ClosestEateryResponse { Command = request.Command, Distance = nearest.Distance, LocationName = nearest.LocationName };
        }
    }

    public class GoogleBarService : BaseHtmlScrapingService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public override ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var service = new GooglePlacesService();
            var nearest = service.Execute(new ClosestGooglePlaceRequest { Command = request.Command, Type = EateryType.bar });
            return new ClosestEateryResponse { Command = request.Command, Distance = nearest.Distance, LocationName = nearest.LocationName };
        }
    }

    public class GoogleRestaurantService : BaseHtmlScrapingService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public override ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var service = new GooglePlacesService();
            var nearest = service.Execute(new ClosestGooglePlaceRequest { Command = request.Command, Type = EateryType.restaurant });
            return new ClosestEateryResponse { Command = request.Command, Distance = nearest.Distance, LocationName = nearest.LocationName };
        }
    }
}