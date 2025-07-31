using Backend.BLL.Services.Interfaces;
using Backend.DAL.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Classes
{
    public class GenericService<TEntity, TEntityRequest, TEntityResponse> : IGenericService<TEntity, TEntityRequest, TEntityResponse> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> repository;
        public GenericService(IGenericRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public async Task<int> AddAsync(TEntityRequest entity)
        {
            var newEntity = entity.Adapt<TEntity>();
            return await repository.AddAsync(newEntity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity =await repository.GetByIdAsync(id);
            if(entity == null)
            {
                return 0;
            }
            return await repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<TEntityResponse>> GetAllAsync()
        {
            var entities =(await repository.GetAllAsync()).Adapt<IEnumerable<TEntityResponse>>();
            return entities;
        }

        public async Task<TEntityResponse> GetByIdAsync(int id)
        {
            var entity = await repository.GetByIdAsync(id);
            if(entity == null)
            {
                return default(TEntityResponse);
            }

            return entity.Adapt<TEntityResponse>();
        }

        public async Task<int> UpdateAsync(int id, TEntityRequest entity)
        {
            var existingEntity = await repository.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return 0; 
            }
            var updatedEntity = entity.Adapt(existingEntity);
            return await repository.UpdateAsync(updatedEntity);
        }
        public async Task<int> DeleteAllAsync()
        {
            var entities = await repository.GetAllAsync();
            if (entities == null || !entities.Any())
            {
                return 0; 
            }
            return await repository.DeleteAllAsync(entities);
        }
    }
}
