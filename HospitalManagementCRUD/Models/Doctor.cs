using System;
using System.Collections.Generic;

namespace HospitalManagementCRUD.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string DoctorName { get; set; } = null!;

    public int? HospitalId { get; set; }

    public int Specialization { get; set; }

    public string? Qualification { get; set; }

    public int? Experience { get; set; }

    public decimal ConsultationFee { get; set; }

    public bool IsAvailable { get; set; }

    public bool IsActive { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Hospital? Hospital { get; set; }

    public virtual SecLoginUser User { get; set; } = null!;
}
