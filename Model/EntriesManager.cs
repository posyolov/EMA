using Repository;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class EntriesManager
    {
        IRepository<Entry> entriesRepository;
        IRepository<EntryReason> reasonsRepository;
        IRepository<EntryContinuationCriteria> continuationCriteriasRepository;

        public event Action EntriesChanged;

        public EntriesManager()
        {
            entriesRepository = new RepositoryEF<Entry>();
            reasonsRepository = new RepositoryEF<EntryReason>();
            continuationCriteriasRepository = new RepositoryEF<EntryContinuationCriteria>();
        }

        public IEnumerable<Entry> GetEntries()
        {
            return entriesRepository.GetWithInclude(par => par.Parent, pos => pos.Position, ps => ps.Parent.Position, r => r.Reason, c => c.ContinuationCriteria, u => u.ChangeUser);
        }

        public IEnumerable<Entry> GetEntriesTree()
        {
            return entriesRepository.GetWithInclude(p => p.ParentId == null, ch => ch.Children, p => p.Parent, pos => pos.Position, ps => ps.Parent.Position, r => r.Reason, c => c.ContinuationCriteria, u => u.ChangeUser);
        }

        public bool AddEntry(Entry entity)
        {
            if (!String.IsNullOrWhiteSpace(entity.Title))
            {
                entriesRepository.Create(entity);
                EntriesChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool UpdateEntry(Entry entity)
        {
            if (!String.IsNullOrWhiteSpace(entity.Title))
            {
                entriesRepository.Update(entity);
                EntriesChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool DeleteEntry(Entry entity)
        {
            entriesRepository.Remove(entity);
            EntriesChanged?.Invoke();
            return true;
        }

        public IEnumerable<EntryReason> GetReasons()
        {
            return reasonsRepository.Get();
        }

        public IEnumerable<EntryContinuationCriteria> GetContinuationCriterias()
        {
            return continuationCriteriasRepository.Get();
        }
    }
}
