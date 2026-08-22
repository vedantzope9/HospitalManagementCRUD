using HospitalManagementCRUD.DTOs;

namespace HospitalManagementCRUD.ServiceLayer.Interfaces
{
    public interface IHospitalService
    {
        Task<ApiResponse<HospitalDTO?>> GetHospitalById(int id);
        Task<ApiResponse<bool?>> SaveHospital(HospitalDTO hospitalDTO);
    }
}
