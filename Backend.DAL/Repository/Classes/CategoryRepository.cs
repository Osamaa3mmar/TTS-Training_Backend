using Backend.DAL.Data;
using Backend.DAL.Models;
using Backend.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Repository.Classes
{
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {

        public CategoryRepository(ApplicationDbContext context):base(context)
        {
            
        }
    }
}
