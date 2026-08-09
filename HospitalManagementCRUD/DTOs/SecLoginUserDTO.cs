namespace HospitalManagementCRUD.DTOs
{
    public class SecLoginUserDTO
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string GenderValue { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? Email { get; set; }

        public required string RoleValue { get; set; }

        public bool IsActive { get; set; }
    }
}
