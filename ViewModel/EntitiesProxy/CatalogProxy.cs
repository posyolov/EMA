using Repository;
using Repository.EF;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ViewModel
{
    public class CatalogProxy : IEntityProxy<CatalogItemVM>
    {
        private readonly IRepository<CatalogItem> repository = new RepositoryEF<CatalogItem>();

        public event Action EntitiesChanged;

        public ObservableCollection<CatalogItemVM> Get()
        {
            var entities = repository.GetWithInclude(i => i.Vendor).OrderBy(n => n.Vendor.Name);
            var entitiesVM = new ObservableCollection<CatalogItemVM>();
            foreach (var item in entities)
                entitiesVM.Add(ViewModelMapper.ToViewModel(item));
            return entitiesVM;
        }

        public bool Add(CatalogItemVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        public bool Update(CatalogItemVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        public bool Delete(CatalogItemVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }
    }
}
