using System;
using System.Linq;

namespace GroundJobs.Data.EF
{
    public class EnterpriseReadyQuery<T> : IEnterpriseReadyQuery<T> where T : class
    {
        private EnterpriseDbContext context;

        public EnterpriseReadyQuery(EnterpriseDbContext context)
        {
            this.context = context;
        }

        public IQueryable<T> Query()
        {
            return context.Set<T>().AsQueryable();
        }
    }
}
