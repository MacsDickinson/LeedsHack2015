using GroundJobs.Services.FoodServices;
using Xunit;

namespace GroundJobs.Services.Tests
{
    public class GreggsServiceSpecification
    {
        [Fact]
        public void ShouldFindChapelAllertonForLS73NU()
        {
            var service = new GreggsService();
            var response = service.Execute(new ClosestEateryRequest { Command = new PostCodeSearchCommand { Postcode = "LS73NU" } });
            Assert.Equal("Chapel Allerton", response.LocationName);
            Assert.Equal(0.261534125F, response.Distance);
        }
    }
}