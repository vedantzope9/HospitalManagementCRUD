using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.Models;
using HospitalManagementCRUD.RepositoryLayer.Interfaces;
using HospitalManagementCRUD.ServiceLayer.Interfaces;
using HospitalManagementCRUD.CommonFunctions;
using AutoMapper;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementCRUD.ServiceLayer.Implementations
{
    public class SecLoginUserService : ISecLoginUserService
    {
        private readonly ISecLoginUserRepo _secLoginUserRepo;
        private readonly MyMapper _myMapper;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ICommonRepo _commonRepo;
        private readonly ICommonServiceCalls _serviceCalls;
        public SecLoginUserService(ISecLoginUserRepo secLoginUserRepo, MyMapper myMapper, IMapper mapper, IConfiguration configuration , ICommonRepo commonRepo, ICommonServiceCalls serviceCalls)
        {
            _secLoginUserRepo = secLoginUserRepo;
            _myMapper = myMapper;
            _mapper = mapper;
            _config = configuration;
            _commonRepo = commonRepo;
            _serviceCalls = serviceCalls;
        }

        public string CreateToken(SecLoginUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.NameIdentifier , user.UserId.ToString()),
                new Claim(ClaimTypes.Role , ((MyEnum.Role)user.Role).ToString())
            };

            var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config.GetValue<string>("AppSettings:Token")!)
                );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                    issuer: _config.GetValue<string>("AppSettings:Issuer"),
                    audience: _config.GetValue<string>("AppSettings:Audience"),
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rand = RandomNumberGenerator.Create();
            rand.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<string> GenerateAndSaveRefreshTokenAsync(SecLoginUser user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(15);
            await _commonRepo.SaveChangesAsyncContext();
            return refreshToken;
        }

        private async Task<SecLoginUser?> ValidateRefreshToken(RefreshTokenRequestDTO dto)
        {
            SecLoginUser? user = await _secLoginUserRepo.GetSecLoginUser(dto.UserId);

            if (user is null || user.RefreshToken != dto.AccessToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }
            return user;
        }

        public async Task<ApiResponse<bool>> RefreshTokenAsync(RefreshTokenRequestDTO dto)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>();

            SecLoginUser? user = await ValidateRefreshToken(dto);
            if (user is null)
            {
                apiResponse.Success = false;
                apiResponse.Data = false;
                apiResponse.Message = "Invalid refresh token";
                return apiResponse;
            }
            apiResponse.Success = true;
            apiResponse.AccessToken = CreateToken(user);
            apiResponse.RefreshToken = await GenerateAndSaveRefreshTokenAsync(user);
            return apiResponse;   
        }

        public async Task<ApiResponse<bool>> CheckLogin(LoginDTO loginDTO)
        {
            ApiResponse<bool> apiResponse=new ApiResponse<bool>();
            SecLoginUser? secLoginUser = await _secLoginUserRepo.CheckLogin(loginDTO.UserName, Common.HashPassword(loginDTO.Password));
            if (secLoginUser != null)
            {
                apiResponse.Success = true;
                apiResponse.Message = "Login successful.";
                apiResponse.AccessToken = CreateToken(secLoginUser);
                apiResponse.RefreshToken = await GenerateAndSaveRefreshTokenAsync(secLoginUser);
            }
            else
            {
                apiResponse.Success = false;
                apiResponse.Message = "Invalid username or password.";
            }
            return apiResponse;
        }

        public async Task<ApiResponse<SecLoginUserDTO?>> GetSecLoginUser(int userId)
        {
            ApiResponse<SecLoginUserDTO?> apiResponse = new ApiResponse<SecLoginUserDTO?>();
            try
            {
                SecLoginUser? secLoginUser = await _secLoginUserRepo.GetSecLoginUser(userId);

                if (secLoginUser == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "No User Found.";
                    return apiResponse;
                }

                //SecLoginUserDTO? secLoginUserDTO=new SecLoginUserDTO();
                //secLoginUserDTO= _myMapper.MapEntityToDto<SecLoginUser, SecLoginUserDTO>(secLoginUser, secLoginUserDTO);

                SecLoginUserDTO secLoginUserDTO = _mapper.Map<SecLoginUserDTO>(secLoginUser);
                secLoginUserDTO.RoleValue = ((MyEnum.Role)secLoginUser.Role).ToString();
                secLoginUserDTO.GenderValue = ((MyEnum.Gender)secLoginUser.Gender).ToString();
                apiResponse.Data = secLoginUserDTO;
                apiResponse.Success = true;
                apiResponse.Message = "User fetched successfully.";
            }
            catch(Exception ex)
            {
                _serviceCalls.Log(MyEnum.LoggerType.WriteAndSaveLog, ex.Message, "GetSecLoginUser", ex.StackTrace);
                apiResponse.Success = false;
                apiResponse.Message = "Error Ocurred";
            }

            return apiResponse;
        }

        public async Task<ApiResponse<bool>> SaveSecLoginUser(SecLoginUserDTO secLoginUserDTO)
        {
            ApiResponse<bool> apiResponse=new ApiResponse<bool>();
            if (secLoginUserDTO.RoleValue == MyEnum.Role.Doctor.ToString())
            {
                apiResponse.Success = false;
                apiResponse.Message = "Please register yourself as Doctor from RegisterAsDoctor endpoint.";
                return apiResponse;
            }

            if (secLoginUserDTO != null && secLoginUserDTO.UserName!=null)
            {
                if(await _secLoginUserRepo.CheckUsernameExists(secLoginUserDTO.UserName))
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Username already exists.";
                    return apiResponse;
                }                    
            }
            secLoginUserDTO.PasswordValue = Common.HashPassword(secLoginUserDTO.PasswordValue);
            SecLoginUser secLoginUser = _mapper.Map<SecLoginUser>(secLoginUserDTO);

            secLoginUser.Gender = (int)Enum.Parse(typeof(MyEnum.Gender) , secLoginUserDTO.GenderValue, true);
            secLoginUser.Role = (int)Enum.Parse(typeof(MyEnum.Role) , secLoginUserDTO.RoleValue, true);
            secLoginUser.Password = secLoginUserDTO.PasswordValue;

            try
            {
                await _secLoginUserRepo.SaveSecLoginUser(secLoginUser);
                apiResponse.Success=true;
                apiResponse.Message = "User saved successfully.";
            }
            catch (Exception ex)
            {
                apiResponse.Success = false ;
                apiResponse.Message = $"Error saving user: {ex.Message}";
                _serviceCalls.Log(MyEnum.LoggerType.WriteAndSaveLog, ex.Message, "SaveSecLoginUser", ex.StackTrace);
            }
            return apiResponse;
        }
    }
}
