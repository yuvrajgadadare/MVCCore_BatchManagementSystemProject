namespace MVCCore_BatchManagementSystemProject.Models
{
    public class BatchModel
    {
        public int BatchId { get; set; }
        public string? BatchName { get; set; }
        public int? TopicId { get; set; }
        public string TopicName { get; set; }
        public int? TrainerId { get; set; }
        public int? TotalStudents { get; set; }
        public string TrainerName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? BatchTime { get; set; }
        public string? Status { get; set; }
        public int total_schedule_days { get; set; }

    }
}
