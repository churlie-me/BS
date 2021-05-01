using BS.Core;
using BS.Data;
using BS.Data.IServices;
using BS.Data.Repositories;
using BS.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        public JobController(BSDBContext _bsdbContext)
        {
            _jobService = new JobService(new JobRepository(_bsdbContext));
        }


        [HttpPost]
        public async Task<bool> Post([FromBody] Job job)
        {
            return await _jobService.AddOrModify(job);
        }

        [HttpPost("listing")]
        public async Task<dboJob> Get(dboJob dboJob)
        {
            try
            {
                return await _jobService.Jobs(dboJob);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<Job> Get(Guid id)
        {
            return await _jobService.Job(id);
        }

    }
}
