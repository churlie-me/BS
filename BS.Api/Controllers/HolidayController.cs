using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BS.Core;
using BS.Data;
using BS.Data.IServices;
using BS.Data.Repositories;
using BS.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : Controller
    {
        #region Private fields & constructor
        private readonly IHolidayService _holidayService;
        #endregion

        public HolidayController(BSDBContext _bsdbContext)
        {
            this._holidayService = new HolidayService(new HolidayRepository(_bsdbContext));
        }

        [HttpGet("{StaffId}")]
        public async Task<List<Holiday>> Holidays(Guid StaffId)
        {
            try
            {
                return await _holidayService.Holidays(StaffId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public async Task<bool> Post([FromBody] Holiday holiday)
        {
            try
            {
                return await _holidayService.AddOrUpdate(holiday);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("store/availability")]
        public async Task<bool> StoreAvailability(DateTime dateTime)
        {
            return await _holidayService.StoreAvailability(dateTime);
        }
    }
}
