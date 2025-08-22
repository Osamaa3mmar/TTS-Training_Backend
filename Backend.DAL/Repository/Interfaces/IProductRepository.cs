using Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Repository.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();
        Task<Product> GetByIdWithCategoryAsync(int id);
    }
}
