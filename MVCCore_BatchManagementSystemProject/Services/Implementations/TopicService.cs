using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;

namespace MVCCore_BatchManagementSystemProject.Services.Implementations
{
    public class TopicService : ITopicService
    {
        private IRepository<TbltrainingTopic> topicRepo;
        private IRepository<TbltrainingCourse> courseRepo;
        private IRepository<TbltrainingCourseTopic> coursetopicRepo;

        public TopicService(IRepository<TbltrainingTopic> topicRepo, IRepository<TbltrainingCourse>courserepo,IRepository<TbltrainingCourseTopic>coursetopicrepo)
        {
            this.topicRepo = topicRepo;
            this.courseRepo = courserepo;
            this.coursetopicRepo = coursetopicrepo;
        }

        public void AddTopic(TbltrainingTopic topic)
        {

            topicRepo.Create(topic);
        }

        public void DeleteTopic(int topic_id)
        {
            topicRepo.Delete(topic_id);

        }

        public List<TopicCourseModel> GetCourseWiseTopics(int course_id)
        {
            List<TopicCourseModel> lst = new List<TopicCourseModel>();
            var query = from c in courseRepo.GetAll()
                        join cq in coursetopicRepo.GetAll() on c.CourseId equals cq.CourseId
                        join t in topicRepo.GetAll() on cq.TopicId equals t.TopicId
                        select new 
                        {
                         topid_id=t.TopicId,
                         topid_name=t.TopicName,
                         course_id=c.CourseId,
                         course_name=c.CourseName
                        };
            foreach(var q in query)
            {
                TopicCourseModel tm = new TopicCourseModel() 
                {
                 CourseId=q.course_id,
                  CourseName=q.course_name,
                   TopicId=q.topid_id,
                    TopicName=q.topid_name
                };
                lst.Add(tm);
            }
            return lst;


        }

        public TbltrainingTopic GetTopic(int topic_id)
        {
            return topicRepo.GetById(topic_id);
        }

        public List<TbltrainingTopic> GetTopics()
        {
            List<TbltrainingTopic> lst = new List<TbltrainingTopic>();
            foreach(var topic in topicRepo.GetAll())
            {
                TbltrainingTopic tp = new TbltrainingTopic() { TopicId=topic.TopicId, TopicName=topic.TopicName };
                lst.Add(tp);
            }
            return lst;
        }

        public void UpdateTopic(TbltrainingTopic topic)
        {
            topicRepo.Update(topic);
        }
    }
}
