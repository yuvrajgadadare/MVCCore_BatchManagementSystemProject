using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class TblstudentRegistration
{
    public int RegistrationId { get; set; }

    public int? StudentId { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public double? Discount { get; set; }

    public int? FeeId { get; set; }

    public virtual TbltrainingCourseFee? Fee { get; set; }

    public virtual TblstudentDetail? Student { get; set; }

    public virtual ICollection<TblbatchStudent> TblbatchStudents { get; set; } = new List<TblbatchStudent>();

    public virtual ICollection<TblscheduleAttendance> TblscheduleAttendances { get; set; } = new List<TblscheduleAttendance>();

    public virtual ICollection<TblstudentPayment> TblstudentPayments { get; set; } = new List<TblstudentPayment>();
}
