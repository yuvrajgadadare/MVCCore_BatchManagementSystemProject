namespace MVCCore_BatchManagementSystemProject.Models
{
    public class StudentBatchModel
    {
        public int StudentId { get; set; }
        public int StudentBatchId { get; set; }
        public string? StudentName { get; set; }
        public int RegistrationId { get; set; }
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public int BatchId { get; set; }
        public string? BatchName { get; set; }
        public int? TopicId { get; set; }
        public string TopicName { get; set; }
        public int? TrainerId { get; set; }
        public string TrainerName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? BatchTime { get; set; }
        public string? Status { get; set; }
    }
}