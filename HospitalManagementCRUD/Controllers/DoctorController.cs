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

        [Authorize(Roles ="Doctor , Admin")]
        [HttpGet]
        public async Task<IActionResult> GetMyInfoAsDoctor()
        {
            var response = await _doctorService.GetMyInfoAsDoctor();
            if (response.Success)
                return Ok(response.Data);
            else
                return NotFound(response.Message);
        }

        [Authorize(Roles ="Admin,User")]
        [HttpPost]
        public async Task<IActionResult> RegisterUserAsDoctor([FromBody] DoctorDTO doctorDTO)
        {
            var response = await _doctorService.RegisterUserAsDoctor(doctorDTO);
            if (response.Success)
                return Ok(response);
            else
                return NotFound(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsDoctor([FromBody] RegisterDoctorDTO registerDoctorDTO)
        {
            var response = await _doctorService.RegisterAsDoctor(registerDoctorDTO);
            if (response.Success)
                return Ok(response.Message);
            else
                return NotFound(response.Message);
        }
    }
}
