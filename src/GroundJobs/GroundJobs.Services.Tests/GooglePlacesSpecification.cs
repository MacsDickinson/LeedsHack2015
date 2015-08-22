using System.Collections.Generic;
using GroundJobs.Services.FoodServices;
using Xunit;

namespace GroundJobs.Services.Tests
{
    public class GooglePlacesServiceSpecification
    {
        [Fact]
        public void ShouldFindSevenForLS73NU()
        {
            var service = new GooglePlacesService();
            var closestGooglePlaceRequest = new ClosestGooglePlaceRequest { Command = new PostCodeSearchCommand { Postcode = "LS73NU" }, Types = new List<GooglePlaceType> { GooglePlaceType.food }};
            var response = service.Execute(closestGooglePlaceRequest);

            Xunit.Assert.Equal("Seven", response.Name);
            Xunit.Assert.Equal((float)53.82792, response.Latitude);
            Xunit.Assert.Equal((float)-1.5374, response.Longitude);
            Xunit.Assert.Equal((float)0.3746797, response.Distance);
        }
    }
}