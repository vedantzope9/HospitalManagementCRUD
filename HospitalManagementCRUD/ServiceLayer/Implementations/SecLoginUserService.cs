using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.Models;
using HospitalManagementCRUD.RepositoryLayer.Interfaces;
using HospitalManagementCRUD.ServiceLayer.Interfaces;
using HospitalManagementCRUD.CommonFunctions;
using AutoMapper;

namespace HospitalManagementCRUD.ServiceLayer.Implementations
{
    public class SecLoginUserService:ISecLoginUserService
    {
        ISecLoginUserRepo _secLoginUserRepo;
        MyMapper _myMapper;
        IMapper _mapper;
        public SecLoginUserService(ISecLoginUserRepo secLoginUserRepo , MyMapper myMapper , IMapper mapper)
        {
            _secLoginUserRepo = secLoginUserRepo;
            _myMapper = myMapper;
            _mapper = mapper;
        }

        public async Task<ApiResponse<SecLoginUserDTO?>> GetSecLoginUser(int userId)
        {
            ApiResponse<SecLoginUserDTO?> apiResponse = new ApiResponse<SecLoginUserDTO?>();
            SecLoginUser? secLoginUser = await _secLoginUserRepo.GetSecLoginUser(userId);

            if (secLoginUser == null)
            {
                apiResponse.Success = false;
                apiResponse.Message = "No User Found.";
                return apiResponse;
            }

            //SecLoginUserDTO? secLoginUserDTO=new SecLoginUserDTO();
            //secLoginUserDTO= _myMapper.MapEntityToDto<SecLoginUser, SecLoginUserDTO>(secLoginUser, secLoginUserDTO);

            SecLoginUserDTO secLoginUserDTO= _mapper.Map<SecLoginUserDTO>(secLoginUser);
            apiResponse.Data = secLoginUserDTO;
            apiResponse.Success = true;
            apiResponse.Message = "User fetched successfully.";

            return apiResponse;
        }

        public async Task<ApiResponse<bool>> SaveSecLoginUser(SecLoginUserDTO secLoginUserDTO)
        {
            ApiResponse<bool> apiResponse=new ApiResponse<bool>();

            if (secLoginUserDTO != null && secLoginUserDTO.UserName!=null)
            {
                if(await _secLoginUserRepo.CheckUsernameExists(secLoginUserDTO.UserName))
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Username already exists.";
                    return apiResponse;
                }                    
            }
            secLoginUserDTO.Password = Common.HashPassword(secLoginUserDTO.Password);
            SecLoginUser secLoginUser = _mapper.Map<SecLoginUser>(secLoginUserDTO);
            try
            {
                await _secLoginUserRepo.SaveSecLoginUser(secLoginUser);
                apiResponse.Success=true;
                apiResponse.Message="User saved successfully.";
            }
            catch (Exception ex)
            {
                apiResponse.Success = false ;
                apiResponse.Message = $"Error saving user: {ex.Message}";
            }
            return apiResponse;
        }
    }
}
