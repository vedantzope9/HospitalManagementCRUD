using HospitalManagementCRUD.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementCRUD.ServiceLayer.Interfaces
{
    public interface IHospitalService
    {
        Task<ApiResponse<HospitalDTO?>> GetHospitalById(int id);
        Task<ApiResponse<bool?>> SaveHospital(HospitalDTO hospitalDTO);
        Task<ApiResponse<List<DoctorDTO>>> GetAllDoctorsOfHospital(int hospitalId);
    }
}
