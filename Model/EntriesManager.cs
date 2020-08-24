using Repository;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class EntriesManager : IEntityManager<Entry>
    {
        readonly IRepository<Entry> entriesRepository = new RepositoryEF<Entry>();
        private readonly IEntityManager<Position> positionsManager;
        private readonly IEntityManager<EntryReason> entryReasonsManager;
        private readonly IEntityManager<EntryContinuationCriteria> entryContinuationCriteriaManager;

        public EntriesManager(IEntityManager<Position> positionsManager, IEntityManager<EntryReason> entryReasonsManager, IEntityManager<EntryContinuationCriteria> entryContinuationCriteriaManager)
        {
            this.positionsManager = positionsManager;
            this.entryReasonsManager = entryReasonsManager;
            this.entryContinuationCriteriaManager = entryContinuationCriteriaManager;
        }

        public event Action EntitiesChanged;

        public object[] RelationEntities
        {
            get => new object[] { Get(), positionsManager.Get(), entryReasonsManager.Get(), entryContinuationCriteriaManager.Get() };
        }

        public IEnumerable<Entry> Get()
        {
            return entriesRepository.GetWithInclude(par => par.Parent, pos => pos.Position, ps => ps.Parent.Position, r => r.Reason, c => c.ContinuationCriteria, u => u.ChangeUser).OrderBy(d => d.OccurDateTime);
        }

        public IEnumerable<Entry> GetEntriesTree()
        {
            //return entriesRepository.GetWithInclude(p => p.ParentId == null, ch => ch.Children, p => p.Parent, pos => pos.Position, r => r.Reason, c => c.ContinuationCriteria, u => u.ChangeUser);
            return entriesRepository.GetWithInclude(false, d => d.OccurDateTime, pos => pos.Position, r => r.Reason, c => c.ContinuationCriteria, u => u.ChangeUser).Where(p => p.ParentId == null);
        }

        public bool Add(Entry entity)
        {
            if (!String.IsNullOrWhiteSpace(entity.Title))
            {
                entriesRepository.Create(entity);
                EntitiesChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool Update(Entry entity)
        {
            if (!String.IsNullOrWhiteSpace(entity.Title))
            {
                entriesRepository.Update(entity);
                EntitiesChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool Delete(Entry entity)
        {
            entriesRepository.Remove(entity);
            EntitiesChanged?.Invoke();
            return true;
        }
    }
}
