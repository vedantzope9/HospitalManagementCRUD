using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService) {
            _appointmentService = appointmentService;
        }

        public async Task<IActionResult> BookAppointment(AppointmentDTO appointmentDTO)
        {
            var response = await _appointmentService.BookAppointment(appointmentDTO);

            if(response.Success==false)
                return NotFound(response.Message);
            return Ok(response);
        }

        [Authorize(Roles = "Admin.Doctor")]
        [HttpGet]
        public async Task<IActionResult> GetMyUpcomingAppointmentAsDoctor()
        {
            var response=await _appointmentService.GetMyUpcomingAppointmentAsDoctor();
            if (response.Success == false)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin.Doctor")]
        [HttpGet]
        public async Task<IActionResult> GetAllMyAppointmentAsDoctor()
        {
            var response = await _appointmentService.GetAllMyAppointmentAsDoctor();
            if (response.Success == false)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }
    }
}
