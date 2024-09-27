using MiniInventoryManagementSystem.Domain.Interfaces;
using MiniInventoryManagementSystem.Infra.Context;
using System.Linq.Expressions;

namespace MiniInventoryManagementSystem.Infra.Repositories
{
    public class Repository : IDisposable, IRepository
    {
        private readonly MiniInventoryManagementSystemCtx _miniInventoryManagementSystemCtx;
        public Repository(MiniInventoryManagementSystemCtx miniInventoryManagementSystemCtx)
        {
            _miniInventoryManagementSystemCtx = miniInventoryManagementSystemCtx;
        }

        public T? Add<T>(T obj) where T : class
        {
            _miniInventoryManagementSystemCtx.Database.BeginTransaction();
            try
            {
                _miniInventoryManagementSystemCtx.Set<T>().Add(obj);
            }
            catch (Exception)
            {
                _miniInventoryManagementSystemCtx.Database.RollbackTransaction();
                return null;
            }

            _miniInventoryManagementSystemCtx.Database.CommitTransaction();
            _miniInventoryManagementSystemCtx.SaveChanges();
            return obj;
        }

        public int? Counter<T>(Expression<Func<T, bool>>? conditional = null) where T : class
        {
            throw new NotImplementedException();
        }

        public bool Delete<T>(T obj) where T : class
        {
            throw new NotImplementedException();
        }

        public List<T>? List<T>(int skip = 0, int take = 50, Expression<Func<T, bool>>? conditional = null) where T : class
        {
            throw new NotImplementedException();
        }

        public List<T>? List<T>(Expression<Func<T, bool>>? conditional = null) where T : class
        {
            throw new NotImplementedException();
        }

        public T? Update<T>(T obj) where T : class
        {
            throw new NotImplementedException();
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _miniInventoryManagementSystemCtx.Dispose();
                }
            }

            _disposed = true;
        }
        #endregion
    }
}
