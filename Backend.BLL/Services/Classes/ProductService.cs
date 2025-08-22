using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Backend.DAL.Models;
using Backend.DAL.Repository.Interfaces;
using Mapster;
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

        public async Task<ICollection<ProductResponse>> GetAllWithCategoryAsync()
        {
            var products = await repo.GetAllWithCategoryAsync();
            return products.Adapt<ICollection<ProductResponse>>();
        }

        public async Task<ProductResponse> GetByIdWithCategoryAsync(int id)
        {
            var product = await repo.GetByIdWithCategoryAsync(id);
            return product.Adapt<ProductResponse>();
        }

        public async Task<bool> ToggleStatusAsync(int id)
        {
            var product=await repo.GetByIdAsync(id);
            if (product == null||product.Quantity<=0)
            {
                return false;
            }
            product.Status=product.Status==Status.Active?Status.InActive:Status.Active;
            await repo.UpdateAsync(product);
            return true;
        }
    }
}
