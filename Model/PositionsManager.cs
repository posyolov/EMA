using Repository;
using Repository.EF;
using System;
using System.Collections.Generic;
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

        public IEnumerable<Position> GetPositions()
        {
            return positionsRepository.GetWithInclude(p => p.Parent, c => c.CatalogItem);
        }

        public bool AddPosition(Position entity)
        {
            if (!positionsRepository.IsExist(e => e.Name == entity.Name) &&
                !String.IsNullOrWhiteSpace(entity.Name))
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
