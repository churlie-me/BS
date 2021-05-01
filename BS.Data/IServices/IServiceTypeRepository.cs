using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IServiceTypeService
    {
        Task<dboServiceType> Types(dboServiceType dboServiceType);
        Task<bool> AddOrUpdate(ServiceType type);
    }
}
