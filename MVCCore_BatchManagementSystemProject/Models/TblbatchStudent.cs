using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class TblbatchStudent
{
    public int BatchStudentId { get; set; }

    public int? BatchId { get; set; }

    public int? RegistrationId { get; set; }

    public virtual Tblbatch? Batch { get; set; }

    public virtual TblstudentRegistration? Registration { get; set; }
}
