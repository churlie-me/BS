using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BS.Core;
using BS.Data;
using BS.Data.IServices;
using BS.Data.Repositories;
using BS.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private IServiceTypeService _serviceTypeService;
        public ServiceTypeController(BSDBContext _bsdbContext)
        {
            _serviceTypeService = new ServiceTypeService(new ServiceTypeRepository(_bsdbContext));
        }

        [HttpPost("sort")]
        public async Task<dboServiceType> Get(dboServiceType dboServiceType)
        {
            try
            {
                return await _serviceTypeService.Types(dboServiceType);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] ServiceType type)
        {
            try
            {
                return await _serviceTypeService.AddOrUpdate(type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}