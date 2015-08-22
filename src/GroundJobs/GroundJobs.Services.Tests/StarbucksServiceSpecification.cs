using Xunit;

namespace GroundJobs.Services.Tests
{
    public class StarbucksServiceSpecification
    {
        [Fact]
        public void ShouldFindTheLightForLS73NU()
        {
            var service = new StarbucksService();
            var response = service.Execute(new GetEateriesRequest {Command = new PostCodeSearchCommand {Postcode = "LS73NU"}});
            Assert.Equal("Leeds - The Light", response.LocationName);
            Assert.Equal(1.89970004558563F, response.Distance);
        } 
    }
}