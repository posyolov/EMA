using System;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public interface IEntityProxy<TEntityVM>
    {
        public event Action EntitiesChanged;
        public ObservableCollection<TEntityVM> Get();
        public bool Add(TEntityVM entity);
        public bool Update(TEntityVM entity);
        public bool Delete(TEntityVM entity);

    }
}
