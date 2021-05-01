using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IRoleRepository
    {
        Role GetRole(Guid RoleId);
        List<Role> Roles();
        Task<bool> Add(Role role);
    }
}
