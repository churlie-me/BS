using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IApplicationService
    {
        Task<bool> AddOrModify(Application application);
        Task<dboApplication> Applications(dboApplication dboApplication);
        Task<Application> Application(Guid id);
    }
}
