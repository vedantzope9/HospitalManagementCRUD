using HospitalManagementCRUD.Models;

namespace HospitalManagementCRUD.RepositoryLayer.Interfaces
{
    public interface IAppointmentRepo
    {
        Task BookAppointment(Appointment appointment);
        Task<List<Appointment>> GetAllAppointmentsByDoctorIdAsync(int doctorId);
        Task<List<Appointment>> GetUpcomingAppointmentsByDoctorIdAsync(int doctorId);
    }
}
