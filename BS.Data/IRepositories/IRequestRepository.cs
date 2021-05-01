using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IRequestRepository
    {
        Task<dborequests> Requests(dborequests dborequests);
        Task<bool> AddOrUpdate(Request request);
    }
}
