using System.Collections.Generic;

namespace GroundJobs.ServiceBus
{
    public class GetEateriesCommand : ICommand
    {
        public IEnumerable<string> FoodPreferences;
    }

    public class GetEateriesResponse : IServiceResponse<GetEateriesCommand>
    {
        public GetEateriesCommand Command { get; private set; }
        public int Distance;
        public bool MatchesFoods;
        public string Address;
    }

    public class PretEateriesService : IService<GetEateriesCommand>
    {
        public IServiceResponse<GetEateriesCommand> Execute(IServiceRequest<GetEateriesCommand> request)
        {

            return new GetEateriesResponse
            {
                Distance = 10
            };
        }
    }
}