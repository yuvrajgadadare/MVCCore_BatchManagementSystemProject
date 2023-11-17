namespace MVCCore_BatchManagementSystemProject.Models
{
    public class TrainerBatchModel
    {
        public int TrainerId { get; set; }

        public string? TrainerName { get; set; }
        public int BatchId { get; set; }

        public string? BatchName { get; set; }

        public int? TopicId { get; set; }
        public string TopicName { get; set; }
    }
}
