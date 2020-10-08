using Repository;
using Repository.EF;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ViewModel
{
    public class PositionsProxy
    {
        private readonly IRepository<Position> repository = new RepositoryEF<Position>();

        public event Action EntitiesChanged;

        public ObservableCollection<PositionFullData> GetPositionsList()
        {
            var entities = repository.GetWithInclude(p => p.Parent, c => c.CatalogItem, v => v.CatalogItem.Vendor);
            var entitiesVM = new ObservableCollection<PositionFullData>();
            foreach (var item in entities)
                entitiesVM.Add(MakeFullData(item));
            return entitiesVM;
        }

        public ObservableCollection<PositionShortData> GetPositionsTree()
        {
            var entities = repository.GetWithInclude(c => c.Children);
            var entitiesVM = new ObservableCollection<PositionShortData>();
            foreach (var item in entities)
            {
                if (item.ParentId == null)
                    entitiesVM.Add(MakeShortData(item));
            }
            return entitiesVM;
        }

        public PositionShortData GetPositionShortData(int? id)
        {
            if (id is null)
                return null;

            var position = repository.FindById((int)id);
            return MakeShortData(position);
        }

        public PositionInfo GetPositionInfo(int? id)
        {
            if (id is null)
                return null;

            var position = repository.FindById((int)id);
            return MakeInfo(position);
        }

        public PositionEdit GetPositionEdit(int? id)
        {
            if (id is null)
                return null;

            var position = repository.FindById((int)id);
            return MakeEdit(position);
        }

        public PositionFullData GetPositionFullData(int? id)
        {
            if (id is null)
                return null;

            var position = repository.FindById((int)id);
            return MakeFullData(position);
        }

        public bool Add(PositionEdit entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        public bool Update(PositionEdit entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        public bool Delete(PositionEdit entityId)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        private PositionShortData MakeShortData(Position position)
        {
            PositionShortData vm = new PositionShortData
            {
                Id = position.Id,
                Name = position.Name,
                Title = position.Title
            };

            if (position.Children != null)
            {
                vm.Children = new ObservableCollection<PositionShortData>();
                foreach (var child in position.Children)
                    vm.Children.Add(MakeShortData(child));
            }

            return vm;
        }

        private PositionInfo MakeInfo(Position position)
        {
            PositionInfo vm = new PositionInfo
            {
                Id = position.Id,
                Name = position.Name,
                Title = position.Title,
                //CatalogItem = entitiesMapper.ToViewModel(position.CatalogItem)
            };

            return vm;
        }

        private PositionEdit MakeEdit(Position position)
        {
            PositionEdit vm = new PositionEdit
            {
                Id = position.Id,
                Name = position.Name,
                Title = position.Title,
                CatalogItemId = position.CatalogItemId
            };

            return vm;
        }

        private PositionFullData MakeFullData(Position position)
        {
            PositionFullData vm = new PositionFullData
            {
                Id = position.Id,
                Name = position.Name,
                Title = position.Title,
                ParentName = position.Parent?.ToString(),
                CatalogItemName = position.CatalogItem?.ToString()
            };

            return vm;
        }
    }
}
