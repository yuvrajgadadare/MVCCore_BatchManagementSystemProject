using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class Tblbatch
{
    public int BatchId { get; set; }

    public string? BatchName { get; set; }

    public int? TopicId { get; set; }

    public int? TrainerId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? BatchTime { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<TblbatchSchedule> TblbatchSchedules { get; set; } = new List<TblbatchSchedule>();

    public virtual ICollection<TblbatchStudent> TblbatchStudents { get; set; } = new List<TblbatchStudent>();

    public virtual TbltrainingTopic? Topic { get; set; }

    public virtual Tbltrainer? Trainer { get; set; }
}
