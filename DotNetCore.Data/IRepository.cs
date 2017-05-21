using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DotNetCore.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
		void Delete(object id);
		void Delete(object id1, object id2);
		void Delete(TEntity entityToDelete);
		//IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
		//                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
		//                            string includeProperties = "");
		IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        TEntity Load(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
		TEntity GetById(object id);
		TEntity GetById(object id1, object id2);
		IQueryable<TEntity> GetByRawSqlQuery(string query, params object[] parameters);
		TEntity Insert(TEntity entity);
		void Update(TEntity entityToUpdate);
    }
}
