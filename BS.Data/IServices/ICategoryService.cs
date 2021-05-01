using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface ICategoryService
    {
        Task<dboCategory> Categories(dboCategory dboCategory);
        Task<bool> Add(Category category);
    }
}
