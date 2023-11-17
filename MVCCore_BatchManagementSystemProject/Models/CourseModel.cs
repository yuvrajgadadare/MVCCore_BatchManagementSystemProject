namespace MVCCore_BatchManagementSystemProject.Models
{
    public class CourseModel
    {
        public int CourseId { get; set; }
        public int FeeId { get; set; }
        public string CourseName { get; set; }
        public string FeeMode { get; set; }
        public float FeeAmount { get; set; }
        public float Gst { get; set; }

    }
}
