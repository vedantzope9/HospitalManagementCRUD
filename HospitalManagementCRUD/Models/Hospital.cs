using System;
using System.Collections.Generic;

namespace HospitalManagementCRUD.Models;

public partial class Hospital
{
    public int HospitalId { get; set; }

    public string HospitalName { get; set; } = null!;

    public string? City { get; set; }

    public string? State { get; set; }

    public int? PinCode { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }
    public int LockId { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
