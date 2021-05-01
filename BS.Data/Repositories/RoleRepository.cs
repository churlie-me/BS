using BS.Core;
using BS.Data.IRepositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private BSDBContext _bsdbContext;
        private RoleManager<Role> _roleManager;
        public RoleRepository(BSDBContext _bsdbContext, RoleManager<Role> _roleManager)
        {
            this._bsdbContext = _bsdbContext;
            this._roleManager = _roleManager;
        }

        public async Task<bool> Add(Role role)
        {
            try
            {
                _bsdbContext.Roles.Add(role);
                var result = await _bsdbContext.SaveChangesAsync();
                return await Task.FromResult(result > 1);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Role GetRole(Guid RoleId)
        {
            try
            {
                return _bsdbContext.Roles.Where(r => r.Id.Equals(RoleId)).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Role> Roles()
        {
            try
            {
                return _bsdbContext.Roles.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
