using BS.Core;
using Central.Data;
using Central.Data.IRepositories;
using Central.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Central.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        #region Private fields & constructor
        private readonly IStoreRepository _storeRepository;
        #endregion

        public StoreController(CDBContext _bsdbContext)
        {
            this._storeRepository = new StoreRepository(_bsdbContext);
        }

        [HttpPost]
        public async Task<bool> AddOrUpdate(Store store)
        {
            return await _storeRepository.AddOrUpdate(store);
        }

        [HttpPost("sort")]
        public async Task<dboStore> Stores(dboStore dboStore)
        {
            return await _storeRepository.Stores(dboStore);
        }
    }
}
