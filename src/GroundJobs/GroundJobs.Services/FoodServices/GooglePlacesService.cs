using System;
using System.Linq;
using GroundJobs.ServiceBus;

namespace GroundJobs.Services.FoodServices
{
    public class GooglePlacesService : BaseHtmlScrapingService<ClosestGooglePlaceRequest, ClosestGooglePlaceResponse>
    {
        public override ClosestGooglePlaceResponse Execute(ClosestGooglePlaceRequest request)
        {
            request.PostCode = request.Command.Postcode.Replace(" ", string.Empty);
            request.APIKey = "AIzaSyCM_pHgWShJwu4sYj_M79lDQ6Tpw9zV_9k";

            return request.Get()?[0];
        }
    }
}