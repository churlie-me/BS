using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BS.Core;

namespace BS.Data.IRepositories
{
    public interface IAppointmentRepository
    {
        Task<bool> AddOrUpdateAppointment(Appointment appointment);
        Task<dboAppointment> Appointments(dboAppointment dboAppointment);
        Task<Appointment> Appointment(Guid appointmentId);
        Task<bool> UpdateAppointment(Appointment appointment);
        Task<dboAppointment> GetAppointments(dboAppointment dboAppointment);
        Task<StylistSchedule> CheckAvailability(StylistSchedule stylistSchedule);
    }
}
