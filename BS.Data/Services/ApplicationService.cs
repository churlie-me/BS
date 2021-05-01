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
    public class JobService : IJobService
    {
        private IJobRepository jobRepository;
        public JobService(JobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        public Task<bool> AddOrModify(Job job)
        {
            return jobRepository.AddOrModify(job);
        }

        public Task<Job> Job(Guid id)
        {
            return jobRepository.Job(id);
        }

        public Task<dboJob> Jobs(dboJob dboJob)
        {
            return jobRepository.Jobs(dboJob);
        }
    }
}
