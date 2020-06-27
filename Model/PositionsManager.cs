using Repository;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PositionsManager
    {
        IRepository<Position> positionsRepository;

        public event Action PositionsChanged;

        public PositionsManager()
        {
            positionsRepository = new RepositoryEF<Position>();
        }

        public Position GetPositionFullData(int id)
        {
            return positionsRepository.GetWithInclude(p => p.Parent, c => c.CatalogItem, v => v.CatalogItem.Vendor).FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Position> GetPositions()
        {
            return positionsRepository.GetWithInclude(p => p.Parent, c => c.CatalogItem, v => v.CatalogItem.Vendor);
        }

        public IEnumerable<Position> GetPositionsTree()
        {
            //return positionsRepository.GetWithInclude(p => p.ParentId == null, ch => ch.Children, p => p.Parent, c => c.CatalogItem, v => v.CatalogItem.Vendor);
            return positionsRepository.Get().Where(p => p.ParentId == null);
        }

        public bool AddPosition(Position entity)
        {
            if (!String.IsNullOrWhiteSpace(entity.Name))
            {
                positionsRepository.Create(entity);
                PositionsChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool UpdatePosition(Position entity)
        {
            if (!String.IsNullOrWhiteSpace(entity.Name))
            {
                positionsRepository.Update(entity);
                PositionsChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool DeletePosition(Position entity)
        {
            positionsRepository.Remove(entity);
            PositionsChanged?.Invoke();
            return true;
        }

    }
}
