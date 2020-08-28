using Repository;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class PositionsManager : IEntityManager<Position>
    {
        readonly IRepository<Position> positionsRepository = new RepositoryEF<Position>();
        private readonly IEntityManager<CatalogItem> catalogManager;

        public PositionsManager(IEntityManager<CatalogItem> catalogManager)
        {
            this.catalogManager = catalogManager;
        }

        public event Action EntitiesChanged;

        public object[] RelationEntities
        {
            get => new object[] { Get(), catalogManager.Get() };
        }

        public IEnumerable<Position> Get()
        {
            return positionsRepository.GetWithInclude(p => p.Parent, c => c.CatalogItem, v => v.CatalogItem.Vendor).OrderBy(n => n.Name);
        }

        public bool Add(Position entity)
        {
            if (!String.IsNullOrWhiteSpace(entity.Name))
            {
                positionsRepository.Create(entity);
                EntitiesChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool Update(Position entity)
        {
            if (!String.IsNullOrWhiteSpace(entity.Name))
            {
                var old = GetPositionFullData(entity.Id);

                positionsRepository.Update(entity);

                //update children names
                //if (entity.Name != old.Name && old.Children != null)
                //{
                //    var positions = positionsRepository.Get(n => n.Name.Contains(old.Name + DELIMITER));
                //    foreach (var item in positions)
                //    {
                //        item.Name = item.Name.Replace(old.Name, entity.Name);
                //        positionsRepository.Update(item);
                //    }
                //}

                EntitiesChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool Delete(Position entity)
        {
            positionsRepository.Remove(entity);
            EntitiesChanged?.Invoke();
            return true;
        }

        public Position GetPositionFullData(int id)
        {
            return positionsRepository.GetWithInclude(p => p.Parent, c => c.CatalogItem, v => v.CatalogItem.Vendor, ch => ch.Children).FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Position> GetPositionsTree()
        {
            return positionsRepository.GetWithInclude(false, n => n.Name, c => c.CatalogItem, v => v.CatalogItem.Vendor).Where(p => p.ParentId == null);
            //return positionsRepository.Get().Where(p => p.ParentId == null);
        }

    }
}
