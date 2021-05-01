using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class BrandService : IBrandService
    {
        private IBrandRepository _brandRepository;
        public BrandService(IBrandRepository _brandRepository)
        {
            this._brandRepository = _brandRepository;
        }

        public async Task<bool> Add(Brand brand)
        {
            return await _brandRepository.Add(brand);
        }

        public async Task<List<Brand>> Brands()
        {
            return await _brandRepository.Brands();
        }
    }
}
