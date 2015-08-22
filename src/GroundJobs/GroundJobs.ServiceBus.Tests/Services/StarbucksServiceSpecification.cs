using GroundJobs.ServiceBus.Services;
using Xunit;

namespace GroundJobs.ServiceBus.Tests.Services
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