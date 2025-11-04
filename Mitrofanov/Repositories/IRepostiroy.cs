using System.Collections.Generic;

namespace StudioStatistic.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        T Create(T entity);
        T Update(T entity);
        bool Delete(int id);
        bool Exists(int id);
    }
}