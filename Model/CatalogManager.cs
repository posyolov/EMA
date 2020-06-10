using Repository;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class CatalogManager
    {
        //EquipmentContext _dbContext;

        IRepository<Vendor> _vendorRepository;
        IRepository<CatalogItem> _catalogRepository;

        public event Action CatalogChanged;
        public event Action VendorsChanged;

        public CatalogManager()
        {
            //_dbContext = new EquipmentContext();

            _vendorRepository = new RepositoryEF<Vendor>(/*_dbContext*/);
            _catalogRepository = new RepositoryEF<CatalogItem>(/*_dbContext*/);
        }

        public IEnumerable<Vendor> GetVendors()
        {
            return _vendorRepository.Get();
        }

        public void AddVendor(Vendor entity)
        {
            _vendorRepository.Create(entity);
            VendorsChanged?.Invoke();
        }

        public void UpdateVendor(Vendor entity)
        {
            _vendorRepository.Update(entity);
            VendorsChanged?.Invoke();
        }

        public void DeleteVendor(Vendor entity)
        {
            _vendorRepository.Remove(entity);
            VendorsChanged?.Invoke();
        }


        public IEnumerable<CatalogItem> GetCatalog()
        {
            return _catalogRepository.GetWithInclude(i => i.Vendor);
        }

        public void AddCatalogItem(CatalogItem entity)
        {
            _catalogRepository.Create(entity);
            CatalogChanged?.Invoke();
        }

        public void UpdateCatalogItem(CatalogItem entity)
        {
            _catalogRepository.Update(entity);
            CatalogChanged?.Invoke();
        }

        public void DeleteCatalogItem(CatalogItem entity)
        {
            _catalogRepository.Remove(entity);
            CatalogChanged?.Invoke();
        }

    }
}
