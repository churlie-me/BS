using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS.Core;
using BS.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BS.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private BSDBContext _bsdbContext;
        public CategoryRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public Task<bool> Add(Category category)
        {
            try
            {
                _bsdbContext.Category.Add(category);
                var result = _bsdbContext.SaveChangesAsync().Result;
                return Task.FromResult(result > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dboCategory> Categories(dboCategory dboCategory)
        {
            try
            {
                dboCategory.Categories = await _bsdbContext.Category.Where(c => c.Type == dboCategory.Type).ToListAsync();
                dboCategory.pageCount = (int)Math.Ceiling((double)_bsdbContext.Category.Where(c => c.Type == dboCategory.Type).Count() / dboCategory.pageSize);

                return dboCategory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
