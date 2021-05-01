using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IReasonRepository
    {
        Task<List<Reason>> Reasons();
        Task<bool> AddOrUpdate(Reason reason);
    }
}
