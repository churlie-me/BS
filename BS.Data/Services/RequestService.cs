using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using BS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class RequestService : IRequestService
    {
        private IRequestRepository _requestRepository;
        public RequestService(RequestRepository _requestRepository)
        {
            this._requestRepository = _requestRepository;
        }

        public async Task<bool> AddOrUpdate(Request request)
        {
            return await _requestRepository.AddOrUpdate(request);
        }

        public async Task<dborequests> Requests(dborequests dborequests)
        {
            return await _requestRepository.Requests(dborequests);
        }
    }
}
