using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IBrandService
    {
        Task<List<Brand>> Brands();
        Task<bool> Add(Brand brand);
    }
}
