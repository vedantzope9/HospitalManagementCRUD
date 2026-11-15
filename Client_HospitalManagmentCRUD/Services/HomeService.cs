using Client_HospitalManagmentCRUD.ServiceCalls;
using HospitalManagementCRUD.DTOs;

namespace Client_HospitalManagmentCRUD.Services
{
    public class HomeService
    {
        private readonly HomeApiClient _homeApiClient;

        public HomeService(HomeApiClient homeApiClient)
        {
            _homeApiClient = homeApiClient;
        }

        public async Task<ApiResponse<bool>?> Login(Client_HospitalManagmentCRUD.Models.DTOs.LoginDTO loginDTO)
        {
            var response = await _homeApiClient.Login(loginDTO);
            return response;
        }
    }
}
