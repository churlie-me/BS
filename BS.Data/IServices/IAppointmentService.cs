using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BS.Core;

namespace BS.Data.IServices
{
    public interface IAppointmentService
    {
        Task<bool> AddOrUpdateAppointment(Appointment appointment);
        Task<Appointment> Appointment(Guid id);
        Task<bool> UpdateAppointment(Appointment appointment);
        Task<dboAppointment> Appointments(dboAppointment dboAppointment);
        Task<dboAppointment> GetAppointments(dboAppointment dboAppointment);
        Task<StylistSchedule> CheckAvailability(StylistSchedule StylistSchedule);
    }
}
