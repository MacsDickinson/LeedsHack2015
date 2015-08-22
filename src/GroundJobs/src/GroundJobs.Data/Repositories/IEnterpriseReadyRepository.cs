using System.Linq;

namespace GroundJobs.Data.Repositories
{
    interface IEnterpriseReadyRepository<T>
    {
        T Get(int id);
        IQueryable<T> Query();
        T Save(T entity);
        void Commit();
    }
}
