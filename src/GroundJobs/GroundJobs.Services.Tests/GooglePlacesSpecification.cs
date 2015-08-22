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
            var response = service.Execute(new ClosestGooglePlaceRequest { Command = new PostCodeSearchCommand { Postcode = "LS73NU" }, Type = EateryType.food});

            Xunit.Assert.Equal("Seven", response.LocationName);
            Xunit.Assert.Equal((float)53.82792, response.Latitude);
            Xunit.Assert.Equal((float)-1.5374, response.Longitude);
            Xunit.Assert.Equal((float)0.3746797, response.Distance);
        }
    }
}