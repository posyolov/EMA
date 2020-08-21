using Repository;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class EntryReasonsManager : IEntityManager<EntryReason>
    {
        readonly IRepository<EntryReason> reasonsRepository = new RepositoryEF<EntryReason>();

        public event Action EntitiesChanged;

        public object[] RelationEntities { get; set; }

        public IEnumerable<EntryReason> Get()
        {
            return reasonsRepository.Get().OrderBy(e => e.Title);
        }

        public bool Add(EntryReason entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(EntryReason entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(EntryReason entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
