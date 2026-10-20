using HospitalManagementCRUD.DTOs;

namespace HospitalManagementCRUD.ServiceLayer.Interfaces
{
    public interface IDoctorService
    {
        Task<ApiResponse<DoctorDTO>> GetMyInfoAsDoctor();
        Task<ApiResponse<bool>> RegisterNewUserAsDoctor(RegisterDoctorDTO registerDoctorDTO);
        Task<ApiResponse<bool>> RegisterExistingUserAsDoctor(DoctorDTO doctorDTO);
    }
}
