namespace GroundJobs.ServiceBus.Services
{
    public class GetEateriesRequest : IServiceRequest<PostCodeSearchCommand>
    {
        public PostCodeSearchCommand Command { get; set; }
    }
}