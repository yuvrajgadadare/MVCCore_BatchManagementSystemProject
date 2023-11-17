namespace MVCCore_BatchManagementSystemProject.Models
{
    public class StudentCourseModel
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public int RegistrationId { get; set; }
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public int? TopicId { get; set; }
        public string TopicName { get; set; }
    }
}
