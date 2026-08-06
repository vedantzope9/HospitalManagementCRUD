using HospitalManagementCRUD.Models;

namespace HospitalManagementCRUD.RepositoryLayer.Interfaces
{
    public interface ISecLoginUserRepo
    {
        Task<SecLoginUser?> GetSecLoginUser(int userId);
        Task SaveSecLoginUser(SecLoginUser secLoginUser);
        Task<bool> CheckUsernameExists(string username);
        Task<bool> CheckLogin(string username, string password);
    }
}
