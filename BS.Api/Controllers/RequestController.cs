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
    public class RequestController : ControllerBase
    {
        private IRequestService _requestService;
        public RequestController(BSDBContext _bsdbContext)
        {
            _requestService = new RequestService(new RequestRepository(_bsdbContext));
        }

        [HttpPost("sort")]
        public async Task<dborequests> Get(dborequests dborequests)
        {
            try
            {
                var requests = await _requestService.Requests(dborequests);

                return requests;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<bool> Post(Request request)
        {
            try
            {
                return await _requestService.AddOrUpdate(request);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
