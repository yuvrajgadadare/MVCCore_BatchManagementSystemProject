﻿using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class TbltrainingCourseFee
{
    public int FeeId { get; set; }

    public int? CourseId { get; set; }

    public double FeesAmount { get; set; }

    public double? Gst { get; set; }

    public string? FeeMode { get; set; }

    public virtual TbltrainingCourse? Course { get; set; }

    public virtual ICollection<TblstudentRegistration> TblstudentRegistrations { get; set; } = new List<TblstudentRegistration>();
}
