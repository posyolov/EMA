using Repository;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Model
{
    public class CatalogManager
    {
        //EquipmentContext _dbContext;

        IRepository<Vendor> vendorRepository;
        IRepository<CatalogItem> catalogRepository;

        public event Action CatalogChanged;
        public event Action VendorsChanged;

        public CatalogManager()
        {
            //_dbContext = new EquipmentContext();

            vendorRepository = new RepositoryEF<Vendor>(/*_dbContext*/);
            catalogRepository = new RepositoryEF<CatalogItem>(/*_dbContext*/);
        }

        public IEnumerable<Vendor> GetVendors()
        {
            return vendorRepository.Get();
        }

        public bool AddVendor(Vendor entity)
        {
            if (!vendorRepository.IsExist(e => e.Name == entity.Name) &&
                !String.IsNullOrWhiteSpace(entity.Name))
            {
                vendorRepository.Create(entity);
                VendorsChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool UpdateVendor(Vendor entity)
        {
            if (!vendorRepository.IsExist(e => e.Name == entity.Name) &&
                !String.IsNullOrWhiteSpace(entity.Name))
            {
                vendorRepository.Update(entity);
                VendorsChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool DeleteVendor(Vendor entity)
        {
            vendorRepository.Remove(entity);
            VendorsChanged?.Invoke();
            return true;
        }


        public IEnumerable<CatalogItem> GetCatalog()
        {
            return catalogRepository.GetWithInclude(i => i.Vendor);
        }

        public bool AddCatalogItem(CatalogItem entity)
        {
            if (!String.IsNullOrWhiteSpace(entity.Title))
            {
                catalogRepository.Create(entity);
                CatalogChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool UpdateCatalogItem(CatalogItem entity)
        {
            if (!String.IsNullOrWhiteSpace(entity.Title))
            {
                catalogRepository.Update(entity);
                CatalogChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool DeleteCatalogItem(CatalogItem entity)
        {
            catalogRepository.Remove(entity);
            CatalogChanged?.Invoke();
            return true;
        }

    }
}
