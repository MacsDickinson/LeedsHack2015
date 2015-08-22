using Xunit;

namespace GroundJobs.Services.Tests
{
    public class PretEateriesServiceServiceSpecification
    {
        [Fact]
        public void ShouldFindLandsLaneForLS73NU()
        {
            var service = new PretEateriesService();
            var response = service.Execute(new GetEateriesRequest { Command = new PostCodeSearchCommand { Postcode = "LS73NU" } });
            Assert.Equal("Leeds, Lands Lane", response.LocationName);
            Assert.Equal(3.3F, response.Distance);
        }
    }
}