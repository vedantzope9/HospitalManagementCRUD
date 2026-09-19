namespace HospitalManagementCRUD.DTOs
{
    public class AppointmentDetailsDTO
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }

        public int PatientAge { get; set; }

        public DateOnly? AppointmentDate { get; set; }

        public TimeOnly? AppointmentTime { get; set; }

        public string? DiseaseDescription { get; set; }

        public decimal FeesPaid { get; set; }
        public string StatusValue { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } 

        public string GenderValue { get; set; }

        public string PhoneNumber { get; set; }

        public string? Email { get; set; }
    }
}
