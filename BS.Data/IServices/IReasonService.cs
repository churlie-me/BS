using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IReasonService
    {
        Task<List<Reason>> Requests();
        Task<bool> AddOrUpdate(Reason reason);
    }
}
