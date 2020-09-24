using Repository;
using Repository.EF;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ViewModel
{
    public class EntriesProxy : IEntityProxy<EntryVM>
    {
        private readonly IRepository<Entry> repository = new RepositoryEF<Entry>();

        public event Action EntitiesChanged;

        public ObservableCollection<EntryVM> Get()
        {
            var entities = repository.GetWithInclude(par => par.Parent, pos => pos.Position, ps => ps.Parent.Position, r => r.Reason, c => c.ContinuationCriteria, u => u.ChangeUser, eu => eu.AssignedUsers).OrderBy(d => d.OccurDateTime);
            var entitiesVM = new ObservableCollection<EntryVM>();
            foreach (var item in entities)
                entitiesVM.Add(ViewModelMapper.ToViewModel(item));
            return entitiesVM;
        }

        public bool Add(EntryVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        public bool Update(EntryVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        public bool Delete(EntryVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }
    }
}
