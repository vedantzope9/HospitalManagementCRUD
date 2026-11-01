using HospitalManagementCRUD.Models;

namespace HospitalManagementCRUD.RepositoryLayer.Interfaces
{
    public interface IAppointmentRepo
    {
        Task BookAppointment(Appointment appointment);
        Task<List<Appointment>> GetAllAppointmentsByDoctorIdAsync(CancellationToken cancellationToken, int doctorId);
        Task<List<Appointment>> GetUpcomingAppointmentsByDoctorIdAsync(int doctorId);
    }
}
