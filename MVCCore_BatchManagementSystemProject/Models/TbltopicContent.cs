using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class TbltopicContent
{
    public int ContentId { get; set; }

    public int? TopicId { get; set; }

    public string? ContentName { get; set; }

    public virtual ICollection<TblbatchSchedule> TblbatchSchedules { get; set; } = new List<TblbatchSchedule>();

    public virtual TbltrainingTopic? Topic { get; set; }
}
