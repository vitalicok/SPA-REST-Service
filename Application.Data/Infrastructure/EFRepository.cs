using Application.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Infrastructure
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        private readonly IDbSet<T> set;

        public EFRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            set = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }

        public IQueryable<T> All()
        {
            return this.set;
        }

        public void Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        public void Delete(object id)
        {
            this.Delete(this.GetById(id));
        }

        public void Dispose()
        {
            this._dbContext.Dispose();
        }

        public T GetById(object id)
        {
            return this.set.Find(id);
        }

        public int SaveChanges()
        {
            return this._dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        private void ChangeEntityState(T entity, EntityState state)
        {
            var entry = this._dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }
            entry.State = state;
        }
    }
}
