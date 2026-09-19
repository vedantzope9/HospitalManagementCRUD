namespace HospitalManagementCRUD.DTOs
{
    public class AppointmentDTO
    {
        public int DoctorId { get; set; }

        public int PatientAge { get; set; }

        public DateOnly? AppointmentDate { get; set; }

        public TimeOnly? AppointmentTime { get; set; }

        public string? DiseaseDescription { get; set; }

        public decimal FeesPaid { get; set; }
    }
}
