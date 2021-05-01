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
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public ApplicationController(BSDBContext _bsdbContext)
        {
            _applicationService = new ApplicationService(new ApplicationRepository(_bsdbContext));
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Application application)
        {
            return await _applicationService.AddOrModify(application);
        }

        [HttpPost("listing")]
        public async Task<dboApplication> Get(dboApplication dboApplication)
        {
            try
            {
                return await _applicationService.Applications(dboApplication);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<Application> Get(Guid id)
        {
            return await _applicationService.Application(id);
        }
    }
}
