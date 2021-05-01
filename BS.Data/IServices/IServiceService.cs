using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BS.Core;

namespace BS.Data.IServices
{
    public interface IServiceService
    {
        Task<bool> AddOrUpdate(Service service);
        Task<Service> Service(Guid serviceId);
        Task<dboService> Services(dboService dboService);
        Task<List<Service>> StoreServices(Guid guid);
    }
}
