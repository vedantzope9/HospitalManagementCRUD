using HospitalManagementCRUD.CommonFunctions;
using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementCRUD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecLoginUserController : ControllerBase
    {
        private readonly ISecLoginUserService _secLoginUserService;
        public SecLoginUserController(ISecLoginUserService secLoginUserService)
        {
            _secLoginUserService = secLoginUserService;
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var response = await _secLoginUserService.GetSecLoginUser(userId);
            return Ok(response);
        }

        [Authorize(Roles = "Doctor,Admin")]
        [HttpGet("doctors-only")]
        public IActionResult DoctorsOnlyEndpoint()
        {
            return Ok("This is only Doctor's endpoint");
        }

        [HttpPost]
        public async Task<IActionResult> SaveSecLoginUser([FromBody]SecLoginUserDTO secLoginUserDTO)
        {
            var response = await _secLoginUserService.SaveSecLoginUser(secLoginUserDTO);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckLogin([FromBody] LoginDTO loginDTO)
        {
            var response = await _secLoginUserService.CheckLogin(loginDTO);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDTO requestDTO)
        {
            var response = await _secLoginUserService.RefreshTokenAsync(requestDTO);

            if(response.Success==false)
            {
                return Unauthorized(response.Message);
            }
            return Ok(response);
        }
    }
}
