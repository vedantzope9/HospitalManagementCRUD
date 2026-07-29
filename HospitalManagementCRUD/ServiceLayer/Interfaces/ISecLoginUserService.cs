using HospitalManagementCRUD.DTOs;

namespace HospitalManagementCRUD.ServiceLayer.Interfaces
{
    public interface ISecLoginUserService
    {
        Task<ApiResponse<SecLoginUserDTO?>> GetSecLoginUser(int userId);
        Task<ApiResponse<bool>> SaveSecLoginUser(SecLoginUserDTO secLoginUserDTO);
    }
}
