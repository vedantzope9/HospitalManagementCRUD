using System;
using System.Collections.Generic;

namespace HospitalManagementCRUD.Models;

public partial class SecLoginUser
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Gender { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public int Role { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
