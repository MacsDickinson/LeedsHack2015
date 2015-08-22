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
            var nearest = service.Execute(new ClosestGooglePlaceRequest { Command = request.Command, Type = EateryType.cafe });
            return nearest != null 
                ? new ClosestEateryResponse { Command = request.Command, Distance = nearest.Distance, LocationName = nearest.LocationName } 
                : default (ClosestEateryResponse);
        }
    }

    public class GoogleBarService : BaseHtmlScrapingService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public override ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var service = new GooglePlacesService();
            var nearest = service.Execute(new ClosestGooglePlaceRequest { Command = request.Command, Type = EateryType.bar });
            return nearest != null
                            ? new ClosestEateryResponse { Command = request.Command, Distance = nearest.Distance, LocationName = nearest.LocationName }
                            : default(ClosestEateryResponse);
        }
    }

    public class GoogleRestaurantService : BaseHtmlScrapingService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public override ClosestEateryResponse Execute(ClosestEateryRequest request)
        {
            var service = new GooglePlacesService();
            var nearest = service.Execute(new ClosestGooglePlaceRequest { Command = request.Command, Type = EateryType.restaurant });
            return nearest != null
                            ? new ClosestEateryResponse { Command = request.Command, Distance = nearest.Distance, LocationName = nearest.LocationName }
                            : default(ClosestEateryResponse);
        }
    }
}