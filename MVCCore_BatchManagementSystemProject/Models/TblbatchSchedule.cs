using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class TblbatchSchedule
{
    public int BatchScheduleId { get; set; }

    public int? BatchId { get; set; }

    public int? ContentId { get; set; }

    public DateTime? ExpectedDate { get; set; }

    public DateTime? ActualDate { get; set; }

    public virtual Tblbatch? Batch { get; set; }

    public virtual TbltopicContent? Content { get; set; }

    public virtual ICollection<TblbatchScheduleAttendance> TblbatchScheduleAttendances { get; set; } = new List<TblbatchScheduleAttendance>();
}
