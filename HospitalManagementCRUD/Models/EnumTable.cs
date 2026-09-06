using System;
using System.Collections.Generic;

namespace HospitalManagementCRUD.Models;

public partial class EnumTable
{
    public int Id { get; set; }

    public string EnumGroup { get; set; } = null!;

    public int EnumId { get; set; }

    public string DisplayText { get; set; } = null!;

    public string? Value { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public bool IsActive { get; set; }
    public int LockId { get; set; }
}
