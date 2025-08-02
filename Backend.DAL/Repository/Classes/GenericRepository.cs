using Backend.DAL.Data;
using Backend.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Repository.Classes
{
    public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> AddAsync(TEntity entity)
        {
           await context.Set<TEntity>().AddAsync(entity);
           return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAllAsync(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
            return await context.SaveChangesAsync();
        }

        public async  Task<int> DeleteAsync(TEntity entity)
        {
             context.Set<TEntity>().Remove(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public Task<IDbContextTransaction> StartTransAction()
        {
            return context.Database.BeginTransactionAsync();
        }

        public async Task<int> UpdateAsync( TEntity entity)
        {
             context.Set<TEntity>().Update(entity);
            return await context.SaveChangesAsync();
        }
    }
}
