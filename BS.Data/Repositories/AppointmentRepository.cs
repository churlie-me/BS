using BS.Core;
using BS.Data.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private BSDBContext _bsdbContext;
        private UserRepository userRepository;
        public AppointmentRepository(BSDBContext _bsdbContext, UserRepository userRepository)
        {
            this._bsdbContext = _bsdbContext;
            this.userRepository = userRepository;
        }

        public async Task<Appointment> Appointment(Guid appointmentId)
        {
            try
            {
                return  await (from a in _bsdbContext.Appointment
                                where a.Id == appointmentId
                                select a).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dboAppointment> Appointments(dboAppointment dboAppointment)
        {
            try
            {
                if(dboAppointment.from != DateTime.MinValue && dboAppointment.to != DateTime.MinValue && dboAppointment.stylistId != Guid.Empty && dboAppointment.branchId != Guid.Empty )
                    dboAppointment.Appointments = await _bsdbContext.Appointment.Include(u => u.User)
                                                                           .Include(u => u.User.Contact)
                                                                            .Include(u => u.User.Address)
                                                                           .Include(x => x.Services)
                                                                           .ThenInclude(s => s.Service)
                                                                           .ThenInclude(s => s.SaleItem)
                                                                           .Include(x => x.Services)
                                                                           .ThenInclude(s => s.Stylist)
                                                                           .Include(b => b.Branch)
                                                                           .Where(x => x.AppointmentDate >= dboAppointment.from.Date && x.AppointmentDate <= dboAppointment.to.Date.AddHours(23) 
                                                                                                                                && x.BranchId == dboAppointment.branchId).ToListAsync();
                else if (dboAppointment.from != DateTime.MinValue && dboAppointment.to != DateTime.MinValue && (dboAppointment.stylistId != Guid.Empty || dboAppointment.branchId != Guid.Empty))
                    dboAppointment.Appointments = await _bsdbContext.Appointment.Include(u => u.User)
                                                                            .Include(u => u.User.Contact)
                                                                            .Include(u => u.User.Address)
                                                                            .Include(x => x.Services)
                                                                            .ThenInclude(s => s.Service)
                                                                            .ThenInclude(s => s.SaleItem)
                                                                            .Include(x => x.Services)
                                                                            .ThenInclude(s => s.Stylist)
                                                                            .Include(b => b.Branch)
                                                                            .Where(x => x.AppointmentDate >= dboAppointment.from.Date && x.AppointmentDate <= dboAppointment.to.Date.AddHours(23)
                                                                                                                                && (x.BranchId == dboAppointment.branchId)).ToListAsync();
                else if (dboAppointment.from != DateTime.MinValue && dboAppointment.to != DateTime.MinValue)
                    dboAppointment.Appointments = await _bsdbContext.Appointment.Include(u => u.User)
                                                                            .Include(u => u.User.Contact)
                                                                            .Include(u => u.User.Address)
                                                                            .Include(x => x.Services)
                                                                            .ThenInclude(s => s.Service)
                                                                            .ThenInclude(s => s.SaleItem)
                                                                            .Include(x => x.Services)
                                                                            .ThenInclude(s => s.Stylist)
                                                                            .Include(b => b.Branch)
                                                                            .Where(x => x.AppointmentDate >= dboAppointment.from.Date && x.AppointmentDate <= dboAppointment.to.Date.AddHours(23)).ToListAsync();
                else if (dboAppointment.stylistId != Guid.Empty || dboAppointment.branchId != Guid.Empty)
                    dboAppointment.Appointments = await _bsdbContext.Appointment.Include(u => u.User)
                                                                            .Include(u => u.User.Contact)
                                                                            .Include(u => u.User.Address)
                                                                            .Include(x => x.Services)
                                                                            .ThenInclude(s => s.Service)
                                                                            .ThenInclude(s => s.SaleItem)
                                                                            .Include(x => x.Services)
                                                                            .ThenInclude(s => s.Stylist)
                                                                            .Include(b => b.Branch)
                                                                            .Where(x => x.BranchId == dboAppointment.branchId).ToListAsync();
                else
                    dboAppointment.Appointments = await _bsdbContext.Appointment.Include(u => u.User)
                                                                            .Include(u => u.User.Contact)
                                                                            .Include(u => u.User.Address)
                                                                            .Include(u => u.Services)
                                                                            .ThenInclude(y => y.Service)
                                                                            .ThenInclude(s => s.SaleItem)
                                                                            .Include(x => x.Services)
                                                                            .ThenInclude(x => x.Stylist)
                                                                            .ThenInclude(z => z.User)
                                                                            .Include(b => b.Branch)
                                                                            .Include(b => b.Branch).ToListAsync();

                return dboAppointment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dboAppointment> GetAppointments(dboAppointment dboAppointment)
        {
            try
            {
                if (!string.IsNullOrEmpty(dboAppointment.search))
                    switch (dboAppointment.column)
                    {
                        default:
                            dboAppointment.Appointments = await _bsdbContext.Appointment
                                                        .Include(u => u.User)
                                                        .Include(u => u.User.Contact)
                                                        .Include(u => u.User.Address)
                                                        .Include(b => b.Branch)
                                                        .Include(v => v.Services)
                                                        .ThenInclude(s => s.Service)
                                                        .ThenInclude(s => s.SaleItem)
                                                        .Include(x => x.Services)
                                                        .ThenInclude(s => s.Stylist)
                                                        .ThenInclude(u => u.User)
                                                        .OrderBy(a => a.AppointmentDate)
                                                        //.Where(x => x.Account.User.FirstName.Contains(dboAppointment.search) || x.Account.User.LastName.Contains(dboAppointment.search))
                                                        .Skip(dboAppointment.pageSize * (dboAppointment.page - 1))
                                                        .Take(dboAppointment.pageSize)
                                                        .ToListAsync();

                            dboAppointment.pageCount = (int)Math.Ceiling((double)_bsdbContext.Appointment//.Include(a => a.Account)
                                                        //.Include(a => a.Account.User)
                                                        //.Where(s => s.Account.User.FirstName.Contains(dboAppointment.search) || s.Account.User.LastName.Contains(dboAppointment.search))
                                                        .Count() / dboAppointment.pageSize);
                            break;
                        case "customer":
                            dboAppointment.Appointments = await _bsdbContext.Appointment.Include(u => u.User)
                                                        .Include(u => u.User.Contact)
                                                        .Include(u => u.User.Address)
                                                        .Include(b => b.Branch)
                                                        .Include(v => v.Services)
                                                        .ThenInclude(s => s.Service)
                                                        .ThenInclude(s => s.SaleItem)
                                                        .Include(x => x.Services)
                                                        .ThenInclude(s => s.Stylist)
                                                        .ThenInclude(u => u.User)
                                                        .OrderBy(a => a.AppointmentDate)
                                                        .Where(x => x.User.FirstName.Contains(dboAppointment.search) || x.User.LastName.Contains(dboAppointment.search))
                                                        .Skip(dboAppointment.pageSize * (dboAppointment.page - 1))
                                                        .Take(dboAppointment.pageSize)
                                                        .ToListAsync();

                            dboAppointment.pageCount = (int)Math.Ceiling((double)_bsdbContext.Appointment
                                                        .Include(a => a.User)
                                                        //.Where(s => s.User.FirstName.Contains(dboAppointment.search) || s.Account.User.LastName.Contains(dboAppointment.search))
                                                        .Count() / dboAppointment.pageSize);
                            break;
                    }
                else if (dboAppointment.from != DateTime.MinValue && dboAppointment.to != DateTime.MinValue)
                {
                    dboAppointment.Appointments = await _bsdbContext.Appointment.Include(u => u.User)
                                                        .Include(u => u.User.Contact)
                                                        .Include(u => u.User.Address)
                                                        .Include(b => b.Branch)
                                                        .Include(v => v.Services)
                                                        .ThenInclude(s => s.Service)
                                                        .ThenInclude(s => s.SaleItem)
                                                        .Include(x => x.Services)
                                                        .ThenInclude(s => s.Stylist)
                                                        .ThenInclude(u => u.User)
                                                        .OrderByDescending(a => a.AppointmentDate)
                                                        .Where(x => x.AppointmentDate >= dboAppointment.from.Date && x.AppointmentDate <= dboAppointment.to.Date.AddHours(23))
                                                        .Skip(dboAppointment.pageSize * (dboAppointment.page - 1))
                                                        .Take(dboAppointment.pageSize)
                                                        .ToListAsync();

                    dboAppointment.pageCount = (int)Math.Ceiling((double)_bsdbContext.Appointment
                                                                                     .Where(x => x.AppointmentDate >= dboAppointment.from.Date && x.AppointmentDate <= dboAppointment.to.Date.AddHours(23))
                                                                                     .Count() / dboAppointment.pageSize);
                }
                else if(dboAppointment.customerId != Guid.Empty || dboAppointment.branchId != Guid.Empty)
                {

                    dboAppointment.Appointments = await _bsdbContext.Appointment.Include(u => u.User)
                                                                                .Include(u => u.User.Address)
                                                                                .Include(u => u.User.Contact)
                                                                                .Include(b => b.Branch)
                                                                                .Include(x => x.Services)
                                                                                .ThenInclude(s => s.Service)
                                                                                .ThenInclude(s => s.SaleItem)
                                                                                .Include(x => x.Services)
                                                                                .ThenInclude(s => s.Stylist)
                                                                                .ThenInclude(u => u.User)
                                                                                .OrderByDescending(a => a.AppointmentDate)
                                                                                .Where(x => (x.BranchId == dboAppointment.branchId || x.UserId == dboAppointment.customerId))
                                                                                .Skip(dboAppointment.pageSize * (dboAppointment.page - 1))
                                                                                .Take(dboAppointment.pageSize)
                                                                                .ToListAsync();

                    dboAppointment.pageCount = (int)Math.Ceiling((double)_bsdbContext.Appointment.Where(x => (x.BranchId == dboAppointment.branchId || x.UserId == dboAppointment.customerId))
                                                                                                 .Count() / dboAppointment.pageSize);
                }
                else
                {

                    dboAppointment.Appointments = await _bsdbContext.Appointment.Include(u => u.User)
                                                                                .Include(u => u.User.Contact)
                                                                                .Include(u => u.User.Address)
                                                                                .Include(b => b.Branch)
                                                                                .Include(x => x.Services)
                                                                                .ThenInclude(s => s.Service)
                                                                                .ThenInclude(s => s.SaleItem)
                                                                                .Include(x => x.Services)
                                                                                .ThenInclude(s => s.Stylist)
                                                                                .ThenInclude(u => u.User)
                                                                                .OrderByDescending(a => a.AppointmentDate)
                                                                                .Skip(dboAppointment.pageSize * (dboAppointment.page - 1))
                                                                                .Take(dboAppointment.pageSize)
                                                                                .ToListAsync();

                    dboAppointment.pageCount = (int)Math.Ceiling((double)_bsdbContext.Appointment.Count() / dboAppointment.pageSize);
                }
                return dboAppointment;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddOrUpdateAppointment(Appointment appointment)
        {
            try
            {
                appointment.Services.ForEach(x => { x.Service = null; x.Stylist = null; });
                if (_bsdbContext.Appointment.Any(p => p.Id == appointment.Id))
                {
                    _bsdbContext.Entry(appointment).State = EntityState.Modified;
                    //Rows
                    foreach (AppointmentService appointmentService in appointment.Services)
                        if (_bsdbContext.AppointmentService.Any(s => s.Id == appointmentService.Id))
                        {
                            _bsdbContext.Entry(appointmentService).State = EntityState.Modified;
                        }
                        else
                            _bsdbContext.AppointmentService.Add(appointmentService);
                }
                else
                {
                    //for existing an user but not registered
                    if (appointment.UserId == Guid.Empty)
                    {
                        if (_bsdbContext.User.Include(y => y.Contact).Any(x => x.Contact.Email == appointment.User.Contact.Email))
                        {
                            appointment.UserId = _bsdbContext.User.Where(x => x.Contact.Email == appointment.User.Contact.Email).FirstOrDefaultAsync().Result.Id;
                            appointment.User = null;
                        }
                    }
                    else
                        appointment.User = null;

                    _bsdbContext.Appointment.Add(appointment);
                }

                return await _bsdbContext.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateAppointment(Appointment appointment)
        {
            try
            {
                _bsdbContext.Entry(appointment).State = EntityState.Modified;
                var result = await _bsdbContext.SaveChangesAsync();
                return result > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<StylistSchedule> CheckAvailability(StylistSchedule StylistSchedule)
        {
            try
            {
                var _stylistSchedule = _bsdbContext.Appointment.Include(a => a.Services.Where(s => s.StylistId == StylistSchedule.StylistId))
                                        .Where(a => a.AppointmentDate >= StylistSchedule.AppointmentTimes.FirstOrDefault().DateTime.Date && a.AppointmentDate <= StylistSchedule.AppointmentTimes.FirstOrDefault().DateTime.Date.AddHours(23)).ToList();

                if(_stylistSchedule.Count() > 0)
                    StylistSchedule.AppointmentTimes = StylistSchedule.AppointmentTimes.Where(p => !_stylistSchedule.Any(x => x.AppointmentDate.TimeOfDay == TimeSpan.Parse(p.Interval))).ToList();
                return StylistSchedule;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
