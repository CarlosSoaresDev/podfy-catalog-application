using podfy_catalog_application.Entities;

namespace podfy_catalog_application.Repository.Base
{
    public interface ICommandRepository<TEntity> : IDisposable where TEntity : EntityBase, new()
    {
        Task<int> SaveAsync(TEntity entity);
        void Save(TEntity entity);
        void Remove(TEntity entity);
        Task RemoveAsync(TEntity entity);
    }
}
