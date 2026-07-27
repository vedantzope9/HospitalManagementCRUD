using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var secLoginUser = await _secLoginUserService.GetSecLoginUser(userId);
            if (secLoginUser == null)
            {
                return NotFound();
            }
            return Ok(secLoginUser);
        }

        

        [HttpPost]
        public async Task<IActionResult> SaveSecLoginUser([FromBody]SecLoginUserDTO secLoginUserDTO)
        {
            if(await _secLoginUserService.SaveSecLoginUser(secLoginUserDTO))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
