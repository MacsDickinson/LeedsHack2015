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
            var response = service.Execute(new ClosestEateryRequest { Command = new PostCodeSearchCommand { Postcode = "LS73NU" } });
            Xunit.Assert.Equal("Seven", response.LocationName);
            Xunit.Assert.Equal("53.827919", response.Latitude);
            Xunit.Assert.Equal("-1.5374", response.Longitude);
            Xunit.Assert.Equal(123, response.Distance);
        }
    }
}