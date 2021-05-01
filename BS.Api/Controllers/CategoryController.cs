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
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(BSDBContext _bsdbContext)
        {
            _categoryService = new CategoryService(new CategoryRepository(_bsdbContext));
        }

        [HttpPost("sort")]
        public async Task<dboCategory> Get(dboCategory dboCategory)
        {
            try
            {
                return await _categoryService.Categories(dboCategory);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Category category)
        {
            try
            {
                return await _categoryService.Add(category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}