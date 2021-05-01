using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private IServiceTypeRepository _serviceTypeRepository;
        public ServiceTypeService(IServiceTypeRepository _serviceTypeRepository)
        {
            this._serviceTypeRepository = _serviceTypeRepository;
        }

        public async Task<bool> AddOrUpdate(ServiceType type)
        {
            return await _serviceTypeRepository.AddOrUpdate(type);
        }

        public async Task<dboServiceType> Types(dboServiceType dboServiceType)
        {
            return await _serviceTypeRepository.Types(dboServiceType);
        }
    }
}
