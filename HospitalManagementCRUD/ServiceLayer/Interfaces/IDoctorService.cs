using HospitalManagementCRUD.DTOs;

namespace HospitalManagementCRUD.ServiceLayer.Interfaces
{
    public interface IDoctorService
    {
        Task<ApiResponse<DoctorDTO>> GetMyInfoAsDoctor();
        Task<ApiResponse<bool>> RegisterAsDoctor(DoctorDTO doctorDTO);
    }
}
