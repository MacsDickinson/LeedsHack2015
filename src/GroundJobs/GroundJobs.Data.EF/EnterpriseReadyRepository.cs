using System.Linq;
using GroundJobs.Data.Repositories;

namespace GroundJobs.Data.EF
{
    public class EnterpriseReadyRepository<T> : IEnterpriseReadyRepository<T> where T : class
    {
        private EnterpriseDbContext context;

        public EnterpriseReadyRepository(EnterpriseDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public T Get(int id)
        {
            return context.Set<T>().Find(id);
        }

        public T Save(T entity)
        {
            return context.Set<T>().Add(entity);
        }
    }
}
