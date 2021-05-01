using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface ICategoryRepository
    {
        Task<dboCategory> Categories(dboCategory dboCategory);
        Task<bool> Add(Category category);
    }
}
