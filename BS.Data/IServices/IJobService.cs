using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IJobService
    {
        Task<bool> AddOrModify(Job Job);
        Task<dboJob> Jobs(dboJob dboJob);
        Task<Job> Job(Guid id);
    }
}
