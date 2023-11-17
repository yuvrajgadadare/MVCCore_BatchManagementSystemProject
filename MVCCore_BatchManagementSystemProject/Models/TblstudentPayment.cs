using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class TblstudentPayment
{
    public int PaymentId { get; set; }

    public int? RegistrationId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public double? PaymentAmount { get; set; }

    public string? PaymentMode { get; set; }

    public string? PaymentDescription { get; set; }

    public virtual TblstudentRegistration? Registration { get; set; }
}
