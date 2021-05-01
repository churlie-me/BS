using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BS.Core;

namespace BS.Data.IRepositories
{
    public interface IServiceRepository
    {
        Task<bool> AddOrUpdate(Service service);
        Task<Service> Service(Guid serviceId);
        Task<dboService> Services(dboService dboService);
        Task<bool> UpdateService(Service service);
        Task<List<Service>> StoreServices(Guid storeId);
    }
}
