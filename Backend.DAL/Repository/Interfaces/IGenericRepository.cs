using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> AddAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> UpdateAsync( TEntity entity);
        Task<int> DeleteAllAsync(IEnumerable<TEntity> entities);
        Task<IDbContextTransaction> StartTransAction(); 

    }
}
