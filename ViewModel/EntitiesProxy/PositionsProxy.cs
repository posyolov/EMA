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

        public ObservableCollection<PositionShortDataVM> GetPositionsTree()
        {
            var entities = repository.GetWithInclude(c => c.Children);
            var entitiesVM = new ObservableCollection<PositionShortDataVM>();
            foreach (var item in entities)
            {
                if (item.ParentId == null)
                    entitiesVM.Add(MakeShortDataVM(item));
            }
            return entitiesVM;
        }

        public PositionInfoVM GetPositionInfoVM(int? id)
        {
            if (id is null)
                return null;

            var position = repository.FindById((int)id);
            return MakeInfoVM(position);
        }

        public PositionEditVM GetPositionEditVM(int? id)
        {
            if (id is null)
                return null;

            var position = repository.FindById((int)id);
            return MakeEditVM(position);
        }


        public bool Add(PositionEditVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        public bool Update(PositionEditVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        public bool Delete(int entityId)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        private PositionShortDataVM MakeShortDataVM(Position position)
        {
            PositionShortDataVM vm = new PositionShortDataVM
            {
                Id = position.Id,
                Name = position.Name,
                Title = position.Title
            };

            if(position.Children != null)
            {
                vm.Children = new ObservableCollection<PositionShortDataVM>();
                foreach (var child in position.Children)
                    vm.Children.Add(MakeShortDataVM(child));
            }

            return vm;
        }

        private PositionInfoVM MakeInfoVM(Position position)
        {
            PositionInfoVM vm = new PositionInfoVM
            {
                Id = position.Id,
                Name = position.Name,
                Title = position.Title,
                //CatalogItem = entitiesMapper.ToViewModel(position.CatalogItem)
            };

            return vm;
        }

        private PositionEditVM MakeEditVM(Position position)
        {
            PositionEditVM vm = new PositionEditVM
            {
                Id = position.Id,
                Name = position.Name,
                Title = position.Title,
                CatalogItemId = position.CatalogItemId
            };

            return vm;
        }

    }
}
