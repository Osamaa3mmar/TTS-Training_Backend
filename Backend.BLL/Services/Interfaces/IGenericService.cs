using Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Interfaces
{
    public interface IGenericService <TEntity,TEntityRequest,TEntityResponse>where TEntity : BaseModel
    {
        public Task<TEntityResponse> GetByIdAsync(int id,bool active=false);
        public Task<IEnumerable<TEntityResponse>> GetAllAsync(bool active=false);
        public Task<int> AddAsync(TEntityRequest entity);
        public Task<int> DeleteAsync(int id);
        public Task<int> UpdateAsync(int id, TEntityRequest entity);
        public Task<int> DeleteAllAsync();
        
    }
}
