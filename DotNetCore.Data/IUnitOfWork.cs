using System;
namespace DotNetCore.Data
{
    public interface IUnitOfWork<T> : IDisposable
    {
		T DBContext();
		IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void SaveChanges();
    }
}
