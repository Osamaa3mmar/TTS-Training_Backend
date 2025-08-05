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

        public async Task<int> DeleteCategoryAsync(int id)
        {
            var res=await categoryRepository.HasProducts(id);
            if (res)
            {
                return 0;
            }
            else
            {
                var cat=await categoryRepository.GetByIdAsync(id);
                return await categoryRepository.DeleteAsync(cat);
            }
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

        public async Task<string> ToggleStatusAsync(int id)
        {
            var cat= await categoryRepository.GetByIdAsync(id);
            if (cat == null)
            {
                return null;
            }
            cat.Status=cat.Status == Status.Active ? Status.InActive : Status.Active;


            return await categoryRepository.UpdateAsync(cat) > 0 ? cat.Status.ToString() : null;
        }
    }
}
