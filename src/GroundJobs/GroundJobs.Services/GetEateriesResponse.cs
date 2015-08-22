using GroundJobs.ServiceBus;

namespace GroundJobs.Services
{
    public class GetEateriesResponse : IServiceResponse<PostCodeSearchCommand>
    {
        public PostCodeSearchCommand Command { get; private set; }
        public float Distance;
        public string LocationName;
    }
}