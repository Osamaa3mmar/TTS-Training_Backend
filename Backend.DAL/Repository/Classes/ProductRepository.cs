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
    public class ProductRepository:GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(ApplicationDbContext context):base(context)
        {
            
        }
    }
}
