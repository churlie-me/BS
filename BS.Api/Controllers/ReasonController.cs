using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BS.Core;
using BS.Data;
using BS.Data.IServices;
using BS.Data.Repositories;
using BS.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReasonController : ControllerBase
    {
        private IReasonService _reasonService;
        public ReasonController(BSDBContext _bsdbContext)
        {
            _reasonService = new ReasonService(new ReasonRepository(_bsdbContext));
        }

        [HttpPost]
        public async Task<bool> Post(Reason reason)
        {
            try
            {
                return await _reasonService.AddOrUpdate(reason);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<List<Reason>> Get()
        {
            try
            {
                var reasons = await _reasonService.Requests();

                return reasons;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
