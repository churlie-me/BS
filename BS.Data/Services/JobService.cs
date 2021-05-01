using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using BS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class ApplicationService : IApplicationService
    {
        private IApplicationRepository applicationRepository;
        public ApplicationService(ApplicationRepository applicationRepository)
        {
            this.applicationRepository = applicationRepository;
        }

        public Task<bool> AddOrModify(Application application)
        {
            return applicationRepository.AddOrModify(application);
        }

        public Task<Application> Application(Guid id)
        {
            return applicationRepository.Application(id);
        }

        public Task<dboApplication> Applications(dboApplication dboApplication)
        {
            return applicationRepository.Applications(dboApplication);
        }
    }
}
