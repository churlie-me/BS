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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        IRoleService _roleService;
        public RoleController(BSDBContext _bsdbContext, RoleManager<Role> _roleManager)
        {
            _roleService = new RoleService(new RoleRepository(_bsdbContext, _roleManager));
        }

        [HttpGet]
        public List<Role> Get()
        {
            try
            {
                return _roleService.Roles();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Role role)
        {
            try
            {
                role.NormalizedName = role.Name;
                return await _roleService.Add(role);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}