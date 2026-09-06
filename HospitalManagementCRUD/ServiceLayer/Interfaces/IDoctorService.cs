using HospitalManagementCRUD.DTOs;

namespace HospitalManagementCRUD.ServiceLayer.Interfaces
{
    public interface IDoctorService
    {
        Task<ApiResponse<DoctorDTO>> GetMyInfoAsDoctor();
        Task<ApiResponse<bool>> RegisterAsDoctor(RegisterDoctorDTO registerDoctorDTO);
        Task<ApiResponse<bool>> RegisterUserAsDoctor(DoctorDTO doctorDTO);
    }
}
