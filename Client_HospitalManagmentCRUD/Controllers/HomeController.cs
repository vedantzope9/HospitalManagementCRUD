using System.Diagnostics;
using System.Threading.Tasks;
using Client_HospitalManagmentCRUD.Models;
using Client_HospitalManagmentCRUD.Models.DTOs;
using Client_HospitalManagmentCRUD.Services;
using Microsoft.AspNetCore.Mvc;

namespace Client_HospitalManagmentCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeService _homeService;

        public HomeController(ILogger<HomeController> logger , HomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            await _homeService.Login(loginDTO);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
