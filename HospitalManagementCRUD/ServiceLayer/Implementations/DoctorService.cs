using System.Security.Claims;
using AutoMapper;
using HospitalManagementCRUD.CommonFunctions;
using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.Models;
using HospitalManagementCRUD.RepositoryLayer.Interfaces;
using HospitalManagementCRUD.ServiceLayer.Interfaces;

namespace HospitalManagementCRUD.ServiceLayer.Implementations
{
    public class DoctorService:IDoctorService
    {
        private readonly IDoctorRepo _doctorRepo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public DoctorService(IDoctorRepo doctorRepo, IMapper mapper, IUserService userService) 
        { 
            _doctorRepo = doctorRepo;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ApiResponse<bool>> RegisterAsDoctor(DoctorDTO doctorDTO)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>();
            Doctor doctor = _mapper.Map<Doctor>(doctorDTO);
            doctor.IsActive = true;
            doctor.UserId = _userService.UserId;
            doctor.Specialization = (int)Enum.Parse(typeof(MyEnum.Specialization),doctorDTO.Specialization);

            try
            {
                await _doctorRepo.SaveDoctorAsync(doctor);
                apiResponse.Success = true;
                apiResponse.Message = "Doctor registered successfully.";
                return apiResponse;
            }
            catch(Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = "Fail to register Doctor.";
                return apiResponse;
            }
        }

        public async Task<ApiResponse<DoctorDTO>> GetMyInfoAsDoctor()
        {
            if(_userService.UserId == 0)
            {
                return new ApiResponse<DoctorDTO>
                {
                    Success = false,
                    Message = "User not found",
                    Data = null
                };
            }
            int userId=_userService.UserId;
            Doctor? doctor = await _doctorRepo.GetDoctorByUserIdAsync(userId);
            if(doctor == null)
            {
                return new ApiResponse<DoctorDTO>
                {
                    Success = false,
                    Message = "Doctor not found",
                    Data = null
                };
            }
            DoctorDTO doctorDTO = _mapper.Map<DoctorDTO>(doctor);
            doctorDTO.Specialization=((MyEnum.Specialization)doctor.Specialization).ToString();
            return new ApiResponse<DoctorDTO>
            {
                Success = true,
                Message = "Doctor found",
                Data = doctorDTO
            };
        }
    }
}
