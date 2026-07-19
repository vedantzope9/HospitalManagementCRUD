using System;
using System.Collections.Generic;

namespace HospitalManagementCRUD.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int DoctorId { get; set; }

    public int PatientAge { get; set; }

    public DateOnly? AppointmentDate { get; set; }

    public TimeOnly? AppointmentTime { get; set; }

    public string? DiseaseDescription { get; set; }

    public int Status { get; set; }

    public decimal FeesPaid { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public int UserId { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual SecLoginUser User { get; set; } = null!;
}
