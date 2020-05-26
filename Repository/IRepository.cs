﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        TEntity FindById(int id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);
        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

    }
}
