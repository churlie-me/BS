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
    public class SeatController : Controller
    {
        private ISeatService _seatService;
        public SeatController(BSDBContext _bsdbContext)
        {
            _seatService = new SeatService(new SeatRepository(_bsdbContext));
        }

        [HttpGet]
        public async Task<List<Seat>> Get()
        {
            return await _seatService.Seats();
        }
    }
}
