namespace HospitalManagementCRUD.DTOs
{
    public class HospitalDTO
    {
        public int HospitalId { get; set; }

        public string HospitalName { get; set; } = null!;

        public string? City { get; set; }

        public string? State { get; set; }

        public int? PinCode { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public bool IsActive { get; set; }
    }
}
