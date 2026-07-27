using HospitalManagementCRUD.DTOs;

namespace HospitalManagementCRUD.ServiceLayer.Interfaces
{
    public interface ISecLoginUserService
    {
        Task<SecLoginUserDTO?> GetSecLoginUser(int userId);
        Task<bool> SaveSecLoginUser(SecLoginUserDTO secLoginUserDTO);
    }
}
