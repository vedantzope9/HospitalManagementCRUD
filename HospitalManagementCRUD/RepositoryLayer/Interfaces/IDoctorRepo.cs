using HospitalManagementCRUD.Models;

namespace HospitalManagementCRUD.RepositoryLayer.Interfaces
{
    public interface IDoctorRepo
    {
        Task<Doctor?> GetDoctorByUserIdAsync(int userId);
        Task SaveDoctorAsync(Doctor doctor);
    }
}
