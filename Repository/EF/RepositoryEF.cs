using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.EF
{
    public class RepositoryEF<TEntity> : IRepository<TEntity> where TEntity : class
    {
        //DbContext _context;
        //DbSet<TEntity> _dbSet;

        public RepositoryEF(/*DbContext context*/)
        {
            //_context = context;
            //_dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            using EquipmentContext context = new EquipmentContext();
            return context.Set<TEntity>()./*AsNoTracking().*/ToList<TEntity>();
            //return _dbSet.AsNoTracking().ToList();
            //return _dbSet.ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            using EquipmentContext context = new EquipmentContext();
            return context.Set<TEntity>().AsNoTracking().Where(predicate).ToList<TEntity>();
            //return _dbSet.AsNoTracking().Where(predicate).ToList();
            //return _dbSet.Where(predicate).ToList();
        }
        public TEntity FindById(int id)
        {
            using EquipmentContext context = new EquipmentContext();
            return context.Set<TEntity>().Find(id);
            //return _dbSet.Find(id);
        }

        public void Create(TEntity item)
        {
            using EquipmentContext context = new EquipmentContext();
            context.Set<TEntity>().Add(item);
            context.SaveChanges();
            //_dbSet.Add(item);
            //_context.SaveChanges();
        }
        public void Update(TEntity item)
        {
            using EquipmentContext context = new EquipmentContext();
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
            //_context.Entry(item).State = EntityState.Modified;
            //_context.SaveChanges();
        }
        public void Remove(TEntity item)
        {
            using EquipmentContext context = new EquipmentContext();
            context.Set<TEntity>().Remove(item);
            context.SaveChanges();
            //_dbSet.Remove(item);
            //_context.SaveChanges();
        }


        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using EquipmentContext context = new EquipmentContext();
            return Include(context, includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using EquipmentContext context = new EquipmentContext();
            var query = Include(context, includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(EquipmentContext context, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = context.Set<TEntity>().AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public bool IsExist(Func<TEntity, bool> predicate)
        {
            using EquipmentContext context = new EquipmentContext();
            return context.Set<TEntity>().AsNoTracking().Count<TEntity>(predicate) != 0;
        }
    }
}
