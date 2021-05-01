using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IBrandRepository
    {
        Task<List<Brand>> Brands();
        Task<bool> Add(Brand brand);
    }
}
