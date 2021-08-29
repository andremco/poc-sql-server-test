using System;

namespace Courses.Domain.Interfaces.Repositories
{
    public interface IEntityBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(TEntity obj);
    }
}
