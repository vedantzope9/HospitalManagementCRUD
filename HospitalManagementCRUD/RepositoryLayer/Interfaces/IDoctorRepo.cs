using HospitalManagementCRUD.Models;

namespace HospitalManagementCRUD.RepositoryLayer.Interfaces
{
    public interface IDoctorRepo
    {
        Task<Doctor?> GetDoctorByUserIdAsync(int userId);
        Task<Doctor?> GetDoctorByDoctorIdAsync(int doctorId);
        Task SaveDoctorAsync(Doctor doctor);
        Task<int> GetDoctorIdByUserIdAsync(int userId);
        Task<List<Doctor>> GetAllDoctorsOfHospital(int hospitalId);
    }
}
