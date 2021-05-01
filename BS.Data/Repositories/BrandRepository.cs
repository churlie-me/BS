using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BS.Core;
using BS.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BS.Data.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private BSDBContext _bsdbContext;
        public BrandRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public Task<bool> Add(Brand brand)
        {
            try
            {
                _bsdbContext.Brand.Add(brand);
                var result = _bsdbContext.SaveChangesAsync().Result;
                return Task.FromResult(result > 0);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Brand>> Brands()
        {
            try
            {
                return await _bsdbContext.Brand.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
