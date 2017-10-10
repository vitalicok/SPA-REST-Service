using Application.Data.Interfaces;
using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Infrastructure
{
    public class RepositoryData : IRepositoryData
    {
        private readonly IDictionary<Type, object> repositories;

        private readonly DbContext _dbContext;

        public RepositoryData() : this(new ApplicationDbContext())
        {
        }

        public RepositoryData(DbContext dbContext)
        {
            _dbContext = dbContext;
            repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users => this.GetRepository<ApplicationUser>();

        public int SaveChanges()
        {
            return this._dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._dbContext.SaveChangesAsync();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EFRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this._dbContext));
            }
            return (IRepository<T>)this.repositories[typeof(T)];
        }
        
    }
}
