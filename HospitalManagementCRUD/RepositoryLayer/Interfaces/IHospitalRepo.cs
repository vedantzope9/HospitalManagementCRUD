using HospitalManagementCRUD.Models;

namespace HospitalManagementCRUD.RepositoryLayer.Interfaces
{
    public interface IHospitalRepo
    {
        Task SaveHospital(Hospital hospital);
        Task<Hospital?> GetHospitalById(int id);
    }
}
