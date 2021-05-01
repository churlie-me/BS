using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IRequestService
    {
        Task<dborequests> Requests(dborequests dborequests);
        Task<bool> AddOrUpdate(Request request);
    }
}
