using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository _categoryRepository)
        {
            this._categoryRepository = _categoryRepository;
        }

        public async Task<bool> Add(Category category)
        {
            return await _categoryRepository.Add(category);
        }

        public async Task<dboCategory> Categories(dboCategory dboCategory)
        {
            return await _categoryRepository.Categories(dboCategory);
        }
    }
}
