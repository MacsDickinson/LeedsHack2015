using System;
using System.Collections.Generic;
using System.Linq;

namespace GroundJobs.Services.FoodServices
{
    public class GoogleCafesService : BaseHtmlScrapingService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public override ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var service = new GooglePlacesService();
            var nearest = service.Execute(new ClosestGooglePlaceRequest { Command = request.Command, Types = { EateryType.cafe } });
            return new ClosestEateryResponse { Command = request.Command, Distance = nearest.Distance, LocationName = nearest.LocationName };
        }
    }

    public class GoogleBarService : BaseHtmlScrapingService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public override ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var service = new GooglePlacesService();
            var nearest = service.Execute(new ClosestGooglePlaceRequest { Command = request.Command, Types = { EateryType.bar } });
            return new ClosestEateryResponse { Command = request.Command, Distance = nearest.Distance, LocationName = nearest.LocationName };
        }
    }

    public class GoogleRestaurantService : BaseHtmlScrapingService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public override ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var service = new GooglePlacesService();
            var nearest = service.Execute(new ClosestGooglePlaceRequest { Command = request.Command, Types = { EateryType.restaurant } });
            return new ClosestEateryResponse { Command = request.Command, Distance = nearest.Distance, LocationName = nearest.LocationName };
        }
    }
}