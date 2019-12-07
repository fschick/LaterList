using FS.LaterList.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FS.LaterList.IoC.Interfaces.Repository.SQLite.Repositories
{
    public interface ILaterListRepository
    {
        List<TResult> Get<TEntity, TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string[] includes = null)
            where TEntity : class, IModel;

        List<TEntity> AddRange<TEntity>(List<TEntity> entities) where TEntity : class, IModel;
    }
}