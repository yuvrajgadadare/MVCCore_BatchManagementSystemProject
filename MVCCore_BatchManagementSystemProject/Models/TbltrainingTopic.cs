using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class TbltrainingTopic
{
    public int TopicId { get; set; }

    public string TopicName { get; set; } = null!;

    public virtual ICollection<Tblbatch> Tblbatches { get; set; } = new List<Tblbatch>();

    public virtual ICollection<TbltopicContent> TbltopicContents { get; set; } = new List<TbltopicContent>();

    public virtual ICollection<TbltrainerTopic> TbltrainerTopics { get; set; } = new List<TbltrainerTopic>();

    public virtual ICollection<TbltrainingCourseTopic> TbltrainingCourseTopics { get; set; } = new List<TbltrainingCourseTopic>();
}
