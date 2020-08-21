using Repository;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class EntryContinuationCriteriaManager : IEntityManager<EntryContinuationCriteria>
    {
        readonly IRepository<EntryContinuationCriteria> continuationCriteriasRepository = new RepositoryEF<EntryContinuationCriteria>();

        public event Action EntitiesChanged;

        public object[] RelationEntities { get; set; }

        public IEnumerable<EntryContinuationCriteria> Get()
        {
            return continuationCriteriasRepository.Get().OrderBy(e => e.Title);
        }

        public bool Add(EntryContinuationCriteria entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(EntryContinuationCriteria entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(EntryContinuationCriteria entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
