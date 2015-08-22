using System;
using GroundJobs.ServiceBus;

namespace GroundJobs.Services.FoodServices
{
    public class ClosestGooglePlaceResponse : IServiceResponse<PostCodeSearchCommand>
    {
        public PostCodeSearchCommand Command { get; }
        public float Latitude;
        public float Longitude;
        public float Distance;
        public string Name;
        public string Icon;
        public string Vicinity;
        public string Rating;

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