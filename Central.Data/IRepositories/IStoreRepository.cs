using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Central.Data.IRepositories
{
    public interface IStoreRepository
    {
        Task<bool> AddOrUpdate(Store store);
        Task<dboStore> Stores(dboStore dboStore);
    }
}
