using FS.LaterList.Common.Interfaces;
using FS.LaterList.IoC.Interfaces.Repository.SQLite.Repositories;
using FS.LaterList.Repository.SQLite.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FS.LaterList.Repository.SQLite.Repositories
{
    public class LaterListRepository : ILaterListRepository
    {
        private readonly LaterListDbContext _dbContext;

        public LaterListRepository(LaterListDbContext dbContext)
            => _dbContext = dbContext;

        public List<TResult> Get<TEntity, TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string[] includes = null)
            where TEntity : class, IModel
            => GetInternal(select, where, orderBy, includes).ToList();

        public TResult FirstOrDefault<TEntity, TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string[] includes = null)
            where TEntity : class, IModel
            => GetInternal(select, where, orderBy, includes).FirstOrDefault();

        public List<TEntity> AddRange<TEntity>(List<TEntity> entities) where TEntity : class, IModel
        {
            var now = DateTime.UtcNow;
            UpdateCreated(entities, now);
            UpdateModified(entities, now);

            var result = entities
                .Select(entity => _dbContext.Add(entity).Entity)
                .ToList();

            _dbContext.SaveChanges();

            return result;
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class, IModel
        {
            var now = DateTime.UtcNow;
            UpdateModified(new[] { entity }, now);

            var result = _dbContext.Update(entity).Entity;
            _dbContext.SaveChanges();

            return result;
        }

        private IQueryable<TResult> GetInternal<TEntity, TResult>(Expression<Func<TEntity, TResult>> select, Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string[] includes) where TEntity : class, IModel
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (where != null)
                query = query.Where(where);

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (orderBy != null)
                query = orderBy(query);

            return query.Select(select);
        }

        private static void UpdateCreated<TEntity>(IEnumerable<TEntity> entities, DateTime? now = null) where TEntity : class, IModel
        {
            now ??= DateTime.UtcNow;
            foreach (var entity in entities.Where(x => x != null))
            {
                entity.Created = now.Value;
                UpdateCreated(GetNavigationValuesImplementsIModel(entity), now);
            }
        }

        private static void UpdateModified<TEntity>(IEnumerable<TEntity> entities, DateTime? now = null) where TEntity : class, IModel
        {
            now ??= DateTime.UtcNow;
            foreach (var entity in entities.Where(x => x != null))
            {
                entity.Modified = now.Value;
                UpdateModified(GetNavigationValuesImplementsIModel(entity), now);
            }
        }

        private static IEnumerable<IModel> GetNavigationValuesImplementsIModel<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
                return Enumerable.Empty<IModel>();

            var oneToManyNavigationProperties = typeof(TEntity)
                .GetProperties()
                .Where(x => typeof(IEnumerable<IModel>).IsAssignableFrom(x.PropertyType))
                .SelectMany(x => (IEnumerable<IModel>)x.GetValue(entity));

            var oneToOneNavigationProperties = typeof(TEntity)
                .GetProperties()
                .Where(x => typeof(IModel).IsAssignableFrom(x.PropertyType))
                .Select(x => (IModel)x.GetValue(entity));

            return oneToManyNavigationProperties.Concat(oneToOneNavigationProperties);
        }
    }
}
