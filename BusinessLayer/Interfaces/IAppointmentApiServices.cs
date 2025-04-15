using EntityLayer.DTOs;
using EntityLayer.Entitys;

namespace BusinessLayer.Interfaces
{
    public interface IAppointmentApiServices
    {
        Task<bool> CreateAppointmentAsync(AppointmentDTO dto);
        Task<List<Appointment>> AppointmentListAsync();
    }
}
