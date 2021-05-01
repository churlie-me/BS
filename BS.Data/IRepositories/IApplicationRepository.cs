using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IApplicationRepository
    {
        Task<bool> AddOrModify(Application application);
        Task<dboApplication> Applications(dboApplication dboAppliation);
        Task<Application> Application(Guid id);
    }
}
