using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class ServiceService: IServiceService
    {
        IServiceRepository _serviceRepository;
        public ServiceService(IServiceRepository _serviceRepository)
        {
            this._serviceRepository = _serviceRepository;
        }

        public async Task<bool> AddOrUpdate(Service service)
        {
            return await _serviceRepository.AddOrUpdate(service);
        }

        public async Task<Service> Service(Guid serviceId)
        {
            return await _serviceRepository.Service(serviceId);
        }

        public async Task<dboService> Services(dboService dboService)
        {
            return await _serviceRepository.Services(dboService);
        }

        public async Task<List<Service>> StoreServices(Guid storeId)
        {
            return await _serviceRepository.StoreServices(storeId);
        }

        public async Task<bool> UpdateService(Service service)
        {
            return await _serviceRepository.UpdateService(service);
        }
    }
}
