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
    public interface ICategoryService:IGenericService<Category,CategoryRequest,CategorySimpleResponse>
    {
        public Task<CategoryFullResponse> GetFullCategoryAsync(int id);
        public Task<int > DeleteCategoryAsync(int id);
        public Task<string> ToggleStatusAsync(int id);
    }
}
