using Microsoft.EntityFrameworkCore;
using StudioStatistic.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudioStatistic.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly APIDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(APIDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll() => _dbSet.ToList();

        public virtual T? GetById(int id) => _dbSet.Find(id);

        public virtual T Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual T Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual bool Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public virtual bool Exists(int id) => _dbSet.Any(e => EF.Property<int>(e, "Id") == id);
    }
}