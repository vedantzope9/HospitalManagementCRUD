namespace HospitalManagementCRUD.DTOs
{
    public class DoctorDTO
    {
        public string DoctorName { get; set; } = null!;

        public string? HospitalValue { get; set; }

        public string SpecializationValue { get; set; }

        public string? Qualification { get; set; }

        public int? Experience { get; set; }

        public decimal ConsultationFee { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsActive { get; set; }

    }
}
