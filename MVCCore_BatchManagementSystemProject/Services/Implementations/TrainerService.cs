using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;

namespace MVCCore_BatchManagementSystemProject.Services.Implementations
{
    public class TrainerService : ITrainerService
    {
        IRepository<Tbltrainer> trainerrepo;
        IRepository<TbltrainerTopic> trainertopicrepo;
        IRepository<TbltrainingTopic> topicrepo;
        public TrainerService(IRepository<Tbltrainer> trainerrepo, IRepository<TbltrainerTopic> trainertopicrepo, IRepository<TbltrainingTopic> topicrepo)
        {
            this.trainerrepo = trainerrepo;
            this.trainertopicrepo = trainertopicrepo;
            this.topicrepo = topicrepo;
        }

        public void AddTrainer(Tbltrainer trainer)
        {

            trainerrepo.Create(trainer);
        }

        public void AddTrainerTopic(TbltrainerTopic trainer_topic)
        {
            trainertopicrepo.Create(trainer_topic);
        }

        public Tbltrainer CheckTrainerLogin(string email_address, string password)
        {
            Tbltrainer t = trainerrepo.GetAll().FirstOrDefault(e => e.EmailAddress.Equals(email_address) & e.Password.Equals(password));

            return t;
        }

        public void DeleteTrainer(int trainer_id)
        {

            trainerrepo.Delete(trainer_id);

        }

        public void DeleteTrainerTopic(int trainer_topic_id)
        {
            trainertopicrepo.Delete(trainer_topic_id);
        }

        public List<TrainerTopicModel> GetTopicWiseTrainers(int topic_id)
        {

            return GetTrainerTopics().Where(e => e.TopicId.Equals(topic_id)).ToList();
        }

        public Tbltrainer GetTrainer(int trainer_id)
        {

            return trainerrepo.GetById(trainer_id);
        }

        public List<Tbltrainer> GetTrainers()
        {
            return trainerrepo.GetAll();

        }

        public List<TrainerTopicModel> GetTrainerTopics()
        {

            List<TrainerTopicModel> lst = new List<TrainerTopicModel>();
            var query = from t in trainerrepo.GetAll()
                        join tp in trainertopicrepo.GetAll() on t.TrainerId equals tp.TrainerId
                        join p in topicrepo.GetAll() on tp.TopicId equals p.TopicId
                        select new {
                         trainer_id=t.TrainerId,
                         trainer_name=t.TrainerName,
                         topic_id=p.TopicId,
                         topic_name=p.TopicName,
                         trainer_topid_id=tp.TrainerTopicId
                        };
            foreach(var q in query)
            {
                TrainerTopicModel tm = new TrainerTopicModel() 
                {
                 TopicId=q.topic_id,
                  TrainerTopicId=q.trainer_topid_id,
                   TopicName=q.topic_name,
                    TrainerId=q.trainer_id,
                     TrainerName=q.trainer_name
                };
                lst.Add(tm);
            }
            return lst;
        }

        public List<TrainerTopicModel> GetTrainerWiseTopics(int trainer_id)
        {
            return GetTrainerTopics().Where(e => e.TrainerId.Equals(trainer_id)).ToList();
        }

        public void UpdateTrainer(Tbltrainer trainer)
        {
            trainerrepo.Update(trainer);
        }

        public void UpdateTrainerTopic(TbltrainerTopic trainer_topic)
        {
           trainertopicrepo.Update(trainer_topic);
        }
    }
}
