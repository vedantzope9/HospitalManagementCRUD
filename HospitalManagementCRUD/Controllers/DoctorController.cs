using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementCRUD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [Authorize(Roles ="Doctor")]
        [HttpGet]
        public async Task<IActionResult> GetMyInfoAsDoctor()
        {
            var response = await _doctorService.GetMyInfoAsDoctor();
            if (response.Success)
                return Ok(response.Data);
            else
                return NotFound(response.Message);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RegisterAsDoctor([FromBody]DoctorDTO doctorDTO)
        {
            var response = await _doctorService.RegisterAsDoctor(doctorDTO);
            if (response.Success)
                return Ok(response.Message);
            else
                return NotFound(response.Message);
        }
    }
}
