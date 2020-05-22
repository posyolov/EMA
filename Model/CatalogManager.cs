using Repository;
using Repository.EF;
using System;
using System.Collections.Generic;

namespace Model
{
    public class CatalogManager
    {
        EquipmentContext _dbContext;

        IRepository<Vendor> _vendorRepository;
        IRepository<CatalogItem> _catalogRepository;

        public CatalogManager()
        {
            _dbContext = new EquipmentContext();

            _vendorRepository = new RepositoryEF<Vendor>(_dbContext);
            _catalogRepository = new RepositoryEF<CatalogItem>(_dbContext);
        }

        public IEnumerable<Vendor> GetVendors()
        {
            return _vendorRepository.Get();
        }

        public IEnumerable<CatalogItem> GetCatalog()
        {
            return _catalogRepository.Get();
        }
    }
}
