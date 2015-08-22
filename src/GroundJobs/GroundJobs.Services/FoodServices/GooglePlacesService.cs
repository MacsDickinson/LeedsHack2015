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

    public class ClosestGooglePlaceResponse : IServiceResponse<PostCodeSearchCommand>
    {
        public PostCodeSearchCommand Command { get; }
        public float Latitude;
        public float Longitude;
        public float Distance;
        public string LocationName;

        public void SetDistanceFrom(float latitude, float longitude)
        {
            var EarthRadius = 6371; // Radius of the earth in km
            var dLat = DegreeToRadius(latitude - Latitude);
            var dLon = DegreeToRadius(longitude - Longitude);
            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreeToRadius(Latitude)) * Math.Cos(DegreeToRadius(latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
                ;
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            Distance = (float)(EarthRadius * c); // Distance in km
        }

        private double DegreeToRadius(float degree)
        {
            return degree * (Math.PI / 180);
        }
    }
}