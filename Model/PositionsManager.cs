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
        const char DELIMITER = ';';
        IRepository<Position> positionsRepository;

        public event Action PositionsChanged;

        public PositionsManager()
        {
            positionsRepository = new RepositoryEF<Position>();
        }

        public Position GetPositionFullData(int id)
        {
            return positionsRepository.GetWithInclude(p => p.Parent, c => c.CatalogItem, v => v.CatalogItem.Vendor, ch => ch.Children).FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Position> GetPositions()
        {
            return positionsRepository.GetWithInclude(p => p.Parent, c => c.CatalogItem, v => v.CatalogItem.Vendor);
        }

        public IEnumerable<Position> GetPositionsTree()
        {
            return positionsRepository.GetWithInclude(c => c.CatalogItem, v => v.CatalogItem.Vendor).Where(p => p.ParentId == null);
            //return positionsRepository.Get().Where(p => p.ParentId == null);
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
                var old = GetPositionFullData(entity.Id);

                positionsRepository.Update(entity);

                //update children names
                if (entity.Name != old.Name && old.Children != null)
                {
                    var positions = positionsRepository.Get(n => n.Name.Contains(old.Name + DELIMITER));
                    foreach (var item in positions)
                    {
                        item.Name = item.Name.Replace(old.Name, entity.Name);
                        positionsRepository.Update(item);
                    }
                }

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
