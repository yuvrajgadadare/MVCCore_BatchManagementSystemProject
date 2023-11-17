using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class TbltrainingCourse
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public virtual ICollection<TbltrainingCourseFee> TbltrainingCourseFees { get; set; } = new List<TbltrainingCourseFee>();

    public virtual ICollection<TbltrainingCourseTopic> TbltrainingCourseTopics { get; set; } = new List<TbltrainingCourseTopic>();
}
