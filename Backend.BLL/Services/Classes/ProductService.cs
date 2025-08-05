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
        private readonly IProductRepository repo;
        public ProductService(IProductRepository repo):base(repo)
        {
            this.repo = repo;
        }

        public async Task<bool> ChangeQuatityById(int id, int quantity)
        {
            if (quantity < 0)
            {
                return false;
            }
            var product = await repo.GetByIdAsync(id);
            if(product == null)
            {
                return false;
            }   
            product.Quantity = quantity;
            product.Status=quantity>0?Status.Active:Status.InActive;
            int res = await repo.UpdateAsync(product);

            return res>0;

        }
    }
}
