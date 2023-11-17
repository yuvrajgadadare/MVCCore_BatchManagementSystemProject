using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class TblbatchScheduleAttendance
{
    public int ScheduleAttendanceId { get; set; }

    public int? BatchScheduleId { get; set; }

    public DateTime? AttendanceDate { get; set; }

    public virtual TblbatchSchedule? BatchSchedule { get; set; }

    public virtual ICollection<TblscheduleAttendance> TblscheduleAttendances { get; set; } = new List<TblscheduleAttendance>();
}
