namespace HospitalManagementCRUD.DTOs
{
    public class RefreshTokenRequestDTO
    {
        public int UserId { get; set; }
        public required string AccessToken { get; set; }
    }
}
