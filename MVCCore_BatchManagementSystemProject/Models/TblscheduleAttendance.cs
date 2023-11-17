using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class TblscheduleAttendance
{
    public int AttendanceId { get; set; }

    public int? ScheduleAttendanceId { get; set; }

    public int? RegistrationId { get; set; }

    public int? IsPresent { get; set; }

    public virtual TblstudentRegistration? Registration { get; set; }

    public virtual TblbatchScheduleAttendance? ScheduleAttendance { get; set; }
}
