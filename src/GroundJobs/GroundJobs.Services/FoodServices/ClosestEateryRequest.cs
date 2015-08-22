using GroundJobs.ServiceBus;

namespace GroundJobs.Services.FoodServices
{
    public class ClosestEateryRequest : IServiceRequest<PostCodeSearchCommand>
    {
        public PostCodeSearchCommand Command { get; set; }
    }

}