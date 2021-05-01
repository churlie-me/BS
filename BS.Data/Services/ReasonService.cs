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
    public class ReasonService : IReasonService
    {
        private IReasonRepository _reasonRepository;
        public ReasonService(ReasonRepository _reasonRepository)
        {
            this._reasonRepository = _reasonRepository;
        }

        public async Task<bool> AddOrUpdate(Reason reason)
        {
            return await _reasonRepository.AddOrUpdate(reason);
        }

        public async Task<List<Reason>> Requests()
        {
            return await _reasonRepository.Reasons();
        }
    }
}
