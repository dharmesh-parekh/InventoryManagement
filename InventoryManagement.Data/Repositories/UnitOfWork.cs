using InventoryManagement.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InventoryManagement.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        IRepository<T> Repository<T>() where T : class, IDisposable;
    }

    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
        }

        public int Commit()
        {
            // Save changes with the default options
            return _dbContext.SaveChanges();
        }

        public IRepository<T> Repository<T>() where T : class, IDisposable
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepository<T>;
            }
            IRepository<T> repo = new Repository<T>(_dbContext);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }

    }
   
}
