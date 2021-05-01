using BS.Core;
using BS.Data;
using BS.Data.IServices;
using BS.Data.Repositories;
using BS.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        #region Private fields & constructor
        private readonly IStoreService _storeService;
        private readonly IScheduleService _scheduleService;
        #endregion

        public StoreController(BSDBContext _bsdbContext)
        {
            this._storeService = new StoreService(new StoreRepository(_bsdbContext));
            _scheduleService = new ScheduleService(new ScheduleRepository(_bsdbContext));
        }

        // GET: api/Store/5
        [HttpGet]
        public async Task<Store> Get()
        {
            try
            {
                var store = await _storeService.Store();
                return store == null? new Store() : store;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("slug/{slugId}")]
        public async Task<Store> StoreBySlug(string slugId)
        {
            try
            {
                return await _storeService.StoreBySlug(slugId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("user/{accountId}")]
        public async Task<List<Store>> GetUserStores(string accountId)
        {
            try
            {
                return await _storeService.UserStores(Guid.Parse(accountId.ToString()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("branches")]
        public async Task<List<Branch>> GetBranches()
        {
            try
            {
                return await _storeService.Branches();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("branch")]
        public async Task<bool> AddOrUpdateBranch(Branch branch)
        {
            try
            {
                return await _storeService.AddOrUpdateBranch(branch);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // POST: api/Store
        [HttpPost]
        public async Task<bool> Post([FromBody] Store store)
        {
            try
            {
                var _store = await _storeService.Store();
                if (_store != null)
                {
                    return await _storeService.Update(store);
                }
                else
                {
                    return await _storeService.Add(store);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("holiday")]
        public async Task<bool> AddSchedule([FromBody] Holiday holiday)
        {
            try
            {
                return await _scheduleService.Add(holiday);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("holiday/fetch/")]
        public async Task<dboHoliday> Holidays(dboHoliday dboHoliday)
        {
            try
            {
                return await _scheduleService.Holidays(dboHoliday);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("schedule")]
        public async Task<List<Schedule>> Schedules()
        {
            try
            {
                return await _scheduleService.Schedules();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("branch/seat/{branchId}")]
        public async Task<List<Seat>> GetSeats(Guid branchId)
        {
            try
            {
                return await _storeService.GetSeats(branchId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/Store/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
