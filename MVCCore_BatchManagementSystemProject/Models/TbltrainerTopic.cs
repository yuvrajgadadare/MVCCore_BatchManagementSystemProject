using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class TbltrainerTopic
{
    public int TrainerTopicId { get; set; }

    public int? TopicId { get; set; }

    public int? TrainerId { get; set; }

    public virtual TbltrainingTopic? Topic { get; set; }

    public virtual Tbltrainer? Trainer { get; set; }
}
