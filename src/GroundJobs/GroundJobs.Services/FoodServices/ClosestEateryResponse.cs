using System;
using GroundJobs.ServiceBus;

namespace GroundJobs.Services.FoodServices
{
    public class ClosestEateryResponse : IServiceResponse<PostCodeSearchCommand>
    {
        public PostCodeSearchCommand Command { get; set; }
        public float Distance;
        public string LocationName;
    }
}