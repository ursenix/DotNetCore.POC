using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DotNetCore.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
		internal DbContext dbContext;
		internal DbSet<T> dbSet;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        private void Save()
        {
            dbContext.SaveChanges();
        }

        public void Delete(object id)
        {
            T _entity = GetById(id);
            dbSet.Remove(_entity);
            Save();
        }

        public void Delete(object id1, object id2)
        {
			T _entity = GetById(id1, id2);
			dbSet.Remove(_entity);
            Save();
        }

        public void Delete(T entityToDelete)
        {
			dbSet.Remove(entityToDelete);
			Save();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
			IQueryable<T> query = dbSet.AsNoTracking();

			if (filter != null)
				query = query.Where(filter);

			foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
																StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

            return query.AsEnumerable();
        }

		// http://stackoverflow.com/questions/43571338/ef-core-helper-method-for-explicit-loading-references-and-collections
		// Usage: Load(customer, e=>e.Orders, e=>e.Returns, e=>e.Account);
		public T Load(T entity, params Expression<Func<T, object>>[] propertyExpressions)
		{
			foreach (var propertyExpression in propertyExpressions)
			{
				var propertyName = propertyExpression.GetPropertyAccess().Name;
				dbContext.Entry<T>(entity).Navigation(propertyName).Load();
			}

            return entity;
		}

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public T GetById(object id1, object id2)
        {
            return dbSet.Find(id1, id2);
        }

        public IQueryable<T> GetByRawSqlQuery(string query, params object[] parameters)
        {
            return dbSet.FromSql<T>(query, parameters);
        }

        public T Insert(T entity)
        {
            dbSet.Add(entity);
            Save();
            return entity;
        }

        public void Update(T entityToUpdate)
        {
            dbSet.Update(entityToUpdate);
            Save();
        }
    }
}
