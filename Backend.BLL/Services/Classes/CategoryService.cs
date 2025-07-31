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
    public class CategoryService:GenericService<Category,CategoryRequest,CategorySimpleResponse>,ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryService(ICategoryRepository repo):base(repo)
        {
            this.categoryRepository = repo;
        }

        public async Task<CategoryFullResponse> GetFullCategoryAsync(int id)
        {
            var cat = await categoryRepository.GetByIdAsync(id);
            if (cat == null)
            {
                return default(CategoryFullResponse);
            }
            return cat.Adapt<CategoryFullResponse>();
        }
    }
}
