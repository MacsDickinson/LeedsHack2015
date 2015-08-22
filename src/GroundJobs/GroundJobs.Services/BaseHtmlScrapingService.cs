using System.Net.Http;
using System.Threading.Tasks;
using GroundJobs.ServiceBus;
using ICommand = GroundJobs.ServiceBus.ICommand;

namespace GroundJobs.Services
{
    public abstract class BaseHtmlScrapingService<TRequest, TResponse> : IService<TRequest, TResponse> where TRequest : IServiceRequest<ICommand> where TResponse : IServiceResponse<ICommand>
    {
        public class Details
        {
            public string Location { get; set; }
            public float Distance { get; set; }
        }
        protected static async Task<string> GetHTMLString(string url)
        {
            var client = new HttpClient() { MaxResponseContentBufferSize = 1000000 };
            return await client.GetStringAsync(url);
        }

        public abstract TResponse Execute(TRequest request);
    }
}