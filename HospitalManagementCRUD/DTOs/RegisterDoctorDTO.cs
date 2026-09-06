namespace HospitalManagementCRUD.DTOs
{
    public class RegisterDoctorDTO
    {
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string GenderValue { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? Email { get; set; }

        public string DoctorName { get; set; } = null!;

        public int? HospitalId { get; set; }

        public string SpecializationValue { get; set; }

        public string? Qualification { get; set; }

        public int? Experience { get; set; }

        public decimal ConsultationFee { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsActive { get; set; }

    }
}
