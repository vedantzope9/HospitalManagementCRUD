using HospitalManagementCRUD.DTOs;
using Client_HospitalManagmentCRUD.Models.DTOs;

namespace Client_HospitalManagmentCRUD.ServiceCalls
{
    public class HomeApiClient
    {
        private readonly HttpClient _httpClient;
        public HomeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<bool>?> Login(Client_HospitalManagmentCRUD.Models.DTOs.LoginDTO loginDTO)
        {
            using var response = await _httpClient.PostAsJsonAsync("api/SecLoginUser/CheckLogin", loginDTO);
            response.EnsureSuccessStatusCode();
            ApiResponse<bool>? apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
            return apiResponse;
        }
    }
}
