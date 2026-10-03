using System;
using System.Collections.Generic;

namespace LoggerService.Models;

public partial class AppExceptionLog
{
    public int LogId { get; set; }

    public string? ExceptionMessage { get; set; }

    public string? SourceMethod { get; set; }

    public string? StackTrace { get; set; }

    public DateTime? LastModifiedDate { get; set; }
}
