using GroundJobs.ServiceBus;

namespace GroundJobs.Services
{
    public class GetEateriesRequest : IServiceRequest<PostCodeSearchCommand>
    {
        public PostCodeSearchCommand Command { get; set; }
    }
}