using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class TblstudentDetail
{
    public int StudentId { get; set; }

    public string? StudentName { get; set; }

    public string? Gender { get; set; }

    public string? MobileNumber { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string? Password { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? ProfilePhoto { get; set; }

    public string? Qualification { get; set; }

    public virtual ICollection<TblstudentRegistration> TblstudentRegistrations { get; set; } = new List<TblstudentRegistration>();
}
