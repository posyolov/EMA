using Repository;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class CatalogManager : IEntityManager<CatalogItem>
    {
        readonly IRepository<CatalogItem> catalogRepository = new RepositoryEF<CatalogItem>();
        private readonly IEntityManager<Vendor> vendorsManager;

        public CatalogManager(IEntityManager<Vendor> vendorsManager)
        {
            this.vendorsManager = vendorsManager;
        }

        public event Action EntitiesChanged;

        public object[] RelationEntities 
        {
            get => new object[] { vendorsManager.Get() };
        }

        public IEnumerable<CatalogItem> Get()
        {
            return catalogRepository.GetWithInclude(i => i.Vendor).OrderBy(n => n.Vendor.Name);
        }

        public bool Add(CatalogItem entity)
        {
            if (!String.IsNullOrWhiteSpace(entity.Title))
            {
                catalogRepository.Create(entity);
                EntitiesChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool Update(CatalogItem entity)
        {
            if (!String.IsNullOrWhiteSpace(entity.Title))
            {
                catalogRepository.Update(entity);
                EntitiesChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool Delete(CatalogItem entity)
        {
            catalogRepository.Remove(entity);
            EntitiesChanged?.Invoke();
            return true;
        }
    }
}
