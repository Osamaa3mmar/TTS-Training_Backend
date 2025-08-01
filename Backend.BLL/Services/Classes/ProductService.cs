using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Backend.DAL.Models;
using Backend.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Classes
{
    public class ProductService:GenericService<Product,ProductRequest,ProductResponse>,IProductService
    {
        public ProductService(IProductRepository repo):base(repo)
        {
            
        }
    }
}
