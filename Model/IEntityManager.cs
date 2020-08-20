using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IEntityManager<TEntity> where TEntity : class
    {
        public event Action EntitiesChanged;
        public IEnumerable<TEntity> Get();
        public bool Add(TEntity entity);
        public bool Update(TEntity entity);
        public bool Delete(TEntity entity);
    }
}
