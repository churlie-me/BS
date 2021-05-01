using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BS.Api.Providers;
using BS.Core;
using BS.Data;
using BS.Data.IRepositories;
using BS.Data.IServices;
using BS.Data.Repositories;
using BS.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentService = BS.Data.Services.AppointmentService;
using UserAccountService = BS.Data.Services.UserAccountService;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private IAppointmentService _appointmentService;
        private IUserAccountService _accountService;
        private IRoleRepository _roleRepository;
        private IUserService _userService;
        private IOrderService _orderService;
        private IServiceService _serviceService;
        private BSDBContext _bsdbContext;

        private UserManager<Account> _userManager;
        public AppointmentController(BSDBContext _bsdbContext, UserManager<Account> _userManager, RoleManager<Role> _roleManager)
        {
            this._bsdbContext = _bsdbContext;
            _appointmentService = new AppointmentService(new AppointmentRepository(_bsdbContext, new UserRepository(_bsdbContext)));
            _roleRepository = new RoleRepository(_bsdbContext, _roleManager);
            _userService = new UserService(new UserRepository(_bsdbContext), _roleRepository, new UserAccountRepository(_bsdbContext));
            _accountService = new UserAccountService(_userManager, _roleManager, _roleRepository, _userService, new UserAccountRepository(_bsdbContext), _bsdbContext);
            _orderService = new OrderService(new OrderRepository(_bsdbContext));
            _serviceService = new ServiceService(new ServiceRepository(_bsdbContext));
            this._userManager = _userManager;
        }

        [HttpPost("calendar")]
        public async Task<dboAppointment> Get(dboAppointment dboAppointment)
        {
            try
            {
                return await _appointmentService.Appointments(dboAppointment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("sort")]
        public async Task<dboAppointment> GetAppointments(dboAppointment dboAppointment)
        {
            try
            {
                var _dboAppointment = await _appointmentService.GetAppointments(dboAppointment);

                return _dboAppointment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("client/{userId}")]
        public async Task<dboAppointment> GetClientAppointments(dboAppointment dboAppointment)
        {
            try
            {
                return await _appointmentService.Appointments(dboAppointment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("detail/{appointmentId}")]
        public async Task<Appointment> GetAppointment(string appointmentId)
        {
            try
            {
                var appointment = await _appointmentService.Appointment(Guid.Parse(appointmentId));

                return appointment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Appointment _appointment)
        {
            try
            {
                var result = await _appointmentService.AddOrUpdateAppointment(_appointment);
                if (result && _appointment.status == AppointmentStatus.Reserved)
                {
                    Store store = await _bsdbContext.Store.FirstOrDefaultAsync();

                    if (!string.IsNullOrEmpty(store.SenderEmail) && !string.IsNullOrEmpty(store.SenderEmailPassword) && !string.IsNullOrEmpty(store.Host))
                    {
                        _appointment.Services.FirstOrDefault().Service = _bsdbContext.Service.FirstOrDefaultAsync(x => x.Id == _appointment.Services.FirstOrDefault().ServiceId).Result;
                        if(_appointment.User == null)
                            _appointment.User = _bsdbContext.User.Include(u => u.Contact).FirstOrDefaultAsync(x => x.Id == _appointment.UserId).Result;
                        //Store Email
                        var subject = "New Reservation";
                        var message = "<h5>Greetings,</h5>"
                                    + "<p> A new reservation for <strong>" + (_appointment.Services.Count() > 0 ? _appointment.Services.FirstOrDefault().Service.Name : "") + "</strong> "
                                    + "has been made by <strong>" + (_appointment.User != null ? _appointment.User.FirstName : _appointment.UserId.ToString()) + "</strong> at " + store.Name
                                    + "</p>";

                        MailProvider.SendMail(store, store.SenderEmail, true, subject, message);

                        //User Email

                        message = "<h5>Greetings sir/madam,</h5>"
                                    + "<p>Your reservation for <strong>" + (_appointment.Services.Count() > 0 ? _appointment.Services.FirstOrDefault().Service.Name : "") + "</strong> "
                                    + "has been received at our " + store.Name + " store"
                                    + "</p>";

                        MailProvider.SendMail(store, _appointment.User.Contact.Email, true, subject, message);
                    }
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("stylist/availability")]
        public async Task<StylistSchedule> CheckStylistAvailability([FromBody] StylistSchedule StylistSchedule)
        {
            return await _appointmentService.CheckAvailability(StylistSchedule);
        }
    }
}