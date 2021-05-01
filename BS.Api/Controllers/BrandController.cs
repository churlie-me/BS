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
    public class BrandController : ControllerBase
    {
        private IBrandService _brandService;
        public BrandController(BSDBContext _bsdbContext)
        {
            _brandService = new BrandService(new BrandRepository(_bsdbContext));
        }

        [HttpGet]
        public async Task<List<Brand>> Get()
        {
            try
            {
                return await _brandService.Brands();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Brand brand)
        {
            try
            {
                return await _brandService.Add(brand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}