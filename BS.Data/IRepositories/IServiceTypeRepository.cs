using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IServiceTypeRepository
    {
        Task<dboServiceType> Types(dboServiceType dboServiceType);
        Task<bool> AddOrUpdate(ServiceType type);
    }
}
