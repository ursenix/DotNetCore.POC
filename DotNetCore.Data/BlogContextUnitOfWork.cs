using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCore.Data
{
    public class BlogContextUnitOfWork : IUnitOfWork<BloggingContext>, IDisposable
    {
        private readonly BloggingContext context;
		private Dictionary<Type, object> repositories;
		private bool disposed = false;

        public BlogContextUnitOfWork(BloggingContext context)
        {
            this.context = context;
			this.repositories = new Dictionary<Type, object>();
			this.disposed = false;
        }

        public BloggingContext DBContext()
		{
			return context;
		}

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)))
                return repositories[typeof(TEntity)] as IRepository<TEntity>;

            var repo = new Repository<TEntity>(context);

            repositories.Add(typeof(TEntity), repo);

            return repo;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}

			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
    }
}
