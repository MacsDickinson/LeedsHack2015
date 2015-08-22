using GroundJobs.ServiceBus;

namespace GroundJobs.Services
{
    public class PostCodeSearchCommand : ICommand
    {
        public string Postcode;
    }
}