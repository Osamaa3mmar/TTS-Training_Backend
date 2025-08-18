using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Interfaces
{
    public  interface IProductService:IGenericService<Product,ProductRequest,ProductResponse>
    {


        
        Task<bool> ChangeQuatityById(int id, int quantity);
        Task<ICollection<ProductResponse>> GetAllWithCategoryAsync();
        Task<bool> ToggleStatusAsync(int id);
    }
}
