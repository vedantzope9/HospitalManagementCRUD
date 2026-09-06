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
        private readonly ISecLoginUserRepo _secLoginUserRepo;
        private readonly ISecLoginUserService _secLoginUserService;
        private readonly ICommonRepo _commonRepo;
        public DoctorService(IDoctorRepo doctorRepo, IMapper mapper, IUserService userService, ISecLoginUserRepo secLoginUserRepo , ICommonRepo commonRepo , ISecLoginUserService secLoginUserService) 
        { 
            _doctorRepo = doctorRepo;
            _mapper = mapper;
            _userService = userService;
            _secLoginUserRepo = secLoginUserRepo;
            _commonRepo = commonRepo;
            _secLoginUserService = secLoginUserService;
        }

        public async Task<ApiResponse<bool>> RegisterAsDoctor(RegisterDoctorDTO registerDoctorDTO)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>();
            try
            {
                registerDoctorDTO.Password= Common.HashPassword(registerDoctorDTO.Password);
                SecLoginUser secLoginUser = _mapper.Map<SecLoginUser>(registerDoctorDTO);

                secLoginUser.Role = (int)MyEnum.Role.Doctor;
                secLoginUser.Gender = (int)Enum.Parse(typeof(MyEnum.Gender), registerDoctorDTO.GenderValue);
                secLoginUser.IsActive = true;

                Doctor doctor = _mapper.Map<Doctor>(registerDoctorDTO);
                doctor.IsActive = true;     
                doctor.Specialization = (int)Enum.Parse(typeof(MyEnum.Specialization), registerDoctorDTO.SpecializationValue);
                doctor.HospitalId = null;

                await _commonRepo.BeginTransactionAsync();
                await _secLoginUserRepo.SaveSecLoginUser(secLoginUser);
                doctor.UserId = secLoginUser.UserId;

                await _doctorRepo.SaveDoctorAsync(doctor);
                await _commonRepo.CommitTransactionAsync();
                apiResponse.Success = true;
                apiResponse.Message = "Doctor registered successfully.";
                return apiResponse;
            }
            catch(Exception ex)
            {
                await _commonRepo.RollBackTransactionAsync();
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
            doctorDTO.SpecializationValue=((MyEnum.Specialization)doctor.Specialization).ToString();
            return new ApiResponse<DoctorDTO>
            {
                Success = true,
                Message = "Doctor found",
                Data = doctorDTO
            };
        }

        public async Task<ApiResponse<bool>> RegisterUserAsDoctor(DoctorDTO doctorDTO)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>();

            try
            {
                Doctor doctor = _mapper.Map<Doctor>(doctorDTO);
                doctor.HospitalId = null;
                doctor.Specialization = (int)(Enum.Parse(typeof(MyEnum.Specialization), doctorDTO.SpecializationValue));
                doctor.UserId = _userService.UserId;

                await _commonRepo.BeginTransactionAsync();
                await _doctorRepo.SaveDoctorAsync(doctor);

                if (_userService.Role == MyEnum.Role.User.ToString())
                {
                    int updatedRecords = await _secLoginUserRepo.ChangeRoleFromUserToDoctor(_userService.UserId);
                    if (updatedRecords != 1)
                    {
                        await _commonRepo.RollBackTransactionAsync();
                        apiResponse.Success = false;
                        apiResponse.Message= "Error Occurred. Failed to change role from User to Doctor.";
                        return apiResponse;
                    }

                    SecLoginUser? secLoginUser = await _secLoginUserRepo.GetSecLoginUser(_userService.UserId);
                    if (secLoginUser != null)
                    {
                        apiResponse.AccessToken = _secLoginUserService.CreateToken(secLoginUser);
                        apiResponse.RefreshToken = await _secLoginUserService.GenerateAndSaveRefreshTokenAsync(secLoginUser);
                    }

                }
                await _commonRepo.CommitTransactionAsync();
                apiResponse.Success = true;
                apiResponse.Message = "User registered as Doctor successfully.";
            }
            catch (Exception ex)
            {
                await _commonRepo.RollBackTransactionAsync();
                apiResponse.Success = false;
            }           
            
            return apiResponse;
        }
    }
}
