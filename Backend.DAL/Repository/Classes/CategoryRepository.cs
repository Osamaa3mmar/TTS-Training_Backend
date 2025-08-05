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
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
        private readonly ApplicationDbContext context;
        public CategoryRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

      

        public async Task<bool> HasProducts(int id)
        {
            var cat = await context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
            if (cat == null)
            {
                return false;
            }
            var res = cat.Products.Any();
            return res;
        }
    }
}
