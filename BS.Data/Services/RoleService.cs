using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class RoleService : IRoleService
    {
        IRoleRepository _roleRepository;
        public RoleService(IRoleRepository _roleRepository)
        {
            this._roleRepository = _roleRepository;
        }

        public async Task<bool> Add(Role role)
        {
            return await _roleRepository.Add(role);
        }

        public List<Role> Roles()
        {
            return _roleRepository.Roles();
        }
    }
}
