using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class AppointmentService : IAppointmentService
    {
        IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository _appointmentRepository)
        {
            this._appointmentRepository = _appointmentRepository;
        }
        public async Task<Appointment> Appointment(Guid AppointmentId)
        {
            return await _appointmentRepository.Appointment(AppointmentId);
        }

        public async Task<dboAppointment> Appointments(dboAppointment dboAppointment)
        {
            return await _appointmentRepository.Appointments(dboAppointment);
        }

        public async Task<dboAppointment> GetAppointments(dboAppointment dboAppointment)
        {
            return await _appointmentRepository.GetAppointments(dboAppointment);
        }

        public async Task<bool> AddOrUpdateAppointment(Appointment appointment)
        {
            return await _appointmentRepository.AddOrUpdateAppointment(appointment);
        }

        public async Task<bool> UpdateAppointment(Appointment appointment)
        {
            return await _appointmentRepository.UpdateAppointment(appointment);
        }

        public async Task<StylistSchedule> CheckAvailability(StylistSchedule StylistSchedule)
        {
            return await _appointmentRepository.CheckAvailability(StylistSchedule);
        }
    }
}
