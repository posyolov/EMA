﻿using Repository;
using Repository.EF;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ViewModel
{
    public class VendorsProxy : IEntityProxy<VendorVM>
    {
        private readonly IRepository<Vendor> repository = new RepositoryEF<Vendor>();
        private readonly EntitiesMapper entitiesMapper;

        public event Action EntitiesChanged;

        public VendorsProxy(EntitiesMapper entitiesMapper)
        {
            this.entitiesMapper = entitiesMapper;
        }

        public ObservableCollection<VendorVM> Get()
        {
            var entities = repository.Get().OrderBy(n => n.Name);
            var entitiesVM = new ObservableCollection<VendorVM>();
            foreach (var item in entities)
                entitiesVM.Add(entitiesMapper.ToViewModel(item));
            return entitiesVM;
        }

        public bool Add(VendorVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        public bool Update(VendorVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }

        public bool Delete(VendorVM entity)
        {
            EntitiesChanged?.Invoke();
            throw new NotImplementedException();
        }
    }
}
