using System.Linq;

namespace GroundJobs.Data.Repositories
{
    public interface IEnterpriseReadyRepository<T> where T : class
    {
        T Get(int id);
        T Save(T entity);
        void Commit();
    }
}
