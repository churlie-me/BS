using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IRoleService
    {
        List<Role> Roles();
        Task<bool> Add(Role role);
    }
}
