using Backend.DAL.Data;
using Backend.DAL.Models;
using Backend.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Repository.Classes
{
    public class ProductRepository:GenericRepository<Product>,IProductRepository
    {
        private readonly  ApplicationDbContext context;
        public ProductRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;    
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        {
                var products=await context.Products.Include(p => p.Category).ToListAsync();
            return products;

        }

       
    }
}
