using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementCRUD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {

        private readonly IHospitalService _hospitalService;
        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        [Authorize]
        [HttpGet("{hospitalId}")]
        public async Task<IActionResult> GetHospitalById(int hospitalId)
        {
            var response= await _hospitalService.GetHospitalById(hospitalId);

            if(response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> SaveHospital([FromBody]HospitalDTO hospitalDTO)
        {
            var response = await _hospitalService.SaveHospital(hospitalDTO);
            if(response.Success)
            {
                return Ok(response.Message);
            }
            else
            {
                return BadRequest(response.Message);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{hospitalId}")]
        public async Task<IActionResult> GetAllDoctorsOfHospital(int hospitalId)
        {
            var response = await _hospitalService.GetAllDoctorsOfHospital(hospitalId);
            if(response.Success == false)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

    }
}
