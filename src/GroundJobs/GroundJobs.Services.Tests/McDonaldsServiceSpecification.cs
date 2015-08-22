using GroundJobs.Services.FoodServices;
using Xunit;

namespace GroundJobs.Services.Tests
{
    public class McDonaldsServiceSpecification
    {
        [Fact]
        public void ShouldFindMcDonaldsLeeds2ForLS73NU()
        {
            var service = new McDonaldsService();
            var response = service.Execute(new ClosestEateryRequest { Command = new PostCodeSearchCommand { Postcode = "LS73NU" } });
            Assert.Equal("McDonald's - Leeds 2", response.LocationName);
            Assert.Equal(2.95023632F, response.Distance);
        }
    }
}