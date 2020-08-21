using Repository;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class VendorsManager : IEntityManager<Vendor>
    {
        readonly IRepository<Vendor> vendorRepository = new RepositoryEF<Vendor>();

        public event Action EntitiesChanged;

        public object[] RelationEntities { get; set; }

        public IEnumerable<Vendor> Get()
        {
            return vendorRepository.Get().OrderBy(n => n.Name);
        }

        public bool Add(Vendor entity)
        {
            if (!vendorRepository.IsExist(e => e.Name == entity.Name) &&
                !String.IsNullOrWhiteSpace(entity.Name))
            {
                vendorRepository.Create(entity);
                EntitiesChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool Update(Vendor entity)
        {
            if (!vendorRepository.IsExist(e => e.Name == entity.Name) &&
                !String.IsNullOrWhiteSpace(entity.Name))
            {
                vendorRepository.Update(entity);
                EntitiesChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool Delete(Vendor entity)
        {
            vendorRepository.Remove(entity);
            EntitiesChanged?.Invoke();
            return true;
        }



    }
}
