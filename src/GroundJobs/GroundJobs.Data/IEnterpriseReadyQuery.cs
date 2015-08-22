using System.Linq;

namespace GroundJobs.Data
{
    public interface IEnterpriseReadyQuery<T> where T : class
    {
        IQueryable<T> Query();
    }
}
