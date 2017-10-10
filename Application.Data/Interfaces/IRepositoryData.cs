using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Interfaces
{
    public interface IRepositoryData
    {
        IRepository<ApplicationUser> Users { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
