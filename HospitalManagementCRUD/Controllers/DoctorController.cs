using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        [Authorize(Roles ="Doctor")]
        [HttpGet]
        public IActionResult GetMyInformationDoctor()
        {

        }
    }
}
