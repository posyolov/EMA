using Repository;
using Repository.EF;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ViewModel
{
    public class PositionsProxy : IEntityProxy<PositionVM>
    {
        private readonly IRepository<Position> repository = new RepositoryEF<Position>();

        public event Action EntitiesChanged;

        public ObservableCollection<PositionVM> Get()
        {
            var entities = repository.GetWithInclude(p => p.Parent, c => c.CatalogItem, v => v.CatalogItem.Vendor).OrderBy(n => n.Name);
            var entitiesVM = new ObservableCollection<PositionVM>();
            foreach (var item in entities)
                entitiesVM.Add(ViewModelMapper.ToViewModel(item));
            return entitiesVM;
        }

        public bool Add(PositionVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        public bool Update(PositionVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        public bool Delete(PositionVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }
    }
}
