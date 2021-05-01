using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IJobRepository
    {
        Task<bool> AddOrModify(Job Job);
        Task<dboJob> Jobs(dboJob dboJob);
        Task<Job> Job(Guid id);
    }
}
