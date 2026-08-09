using HospitalManagementCRUD.DTOs;

namespace HospitalManagementCRUD.ServiceLayer.Interfaces
{
    public interface ISecLoginUserService
    {
        Task<ApiResponse<SecLoginUserDTO?>> GetSecLoginUser(int userId);
        Task<ApiResponse<bool>> SaveSecLoginUser(SecLoginUserDTO secLoginUserDTO);
        Task<ApiResponse<bool>> CheckLogin(LoginDTO loginDTO);
        Task<ApiResponse<bool>> RefreshTokenAsync(RefreshTokenRequestDTO dto);
    }
}
