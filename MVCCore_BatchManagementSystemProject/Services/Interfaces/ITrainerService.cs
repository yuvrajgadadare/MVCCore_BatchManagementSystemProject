using MVCCore_BatchManagementSystemProject.Models;

namespace MVCCore_BatchManagementSystemProject.Services.Interfaces
{
    public interface ITrainerService
    {
        void AddTrainer(Tbltrainer trainer);
        void UpdateTrainer(Tbltrainer trainer);
        void DeleteTrainer(int trainer_id);
        List<Tbltrainer> GetTrainers();
        Tbltrainer GetTrainer(int trainer_id);
        void AddTrainerTopic(TbltrainerTopic trainer_topic);
        void UpdateTrainerTopic(TbltrainerTopic trainer_topic);
        void DeleteTrainerTopic(int trainer_topic_id);
        List<TrainerTopicModel> GetTrainerTopics();
        List<TrainerTopicModel> GetTrainerWiseTopics(int trainer_id);
        List<TrainerTopicModel> GetTopicWiseTrainers(int topic_id);
        Tbltrainer CheckTrainerLogin(string email_address, string password);
    }
}
