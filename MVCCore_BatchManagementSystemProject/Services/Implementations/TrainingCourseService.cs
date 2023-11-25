using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;

namespace MVCCore_BatchManagementSystemProject.Services.Implementations
{
    public class TrainingCourseService : ITrainingCourseService
    {

        IRepository<TbltrainingCourse> courserepo;
        IRepository<TbltrainingCourseFee> feerepo;
        IRepository<TbltrainingCourseTopic> coursetopicrepo;
        IRepository<TbltrainingTopic> topicepo;
        IRepository<TbltopicContent> contentrepo;
        public TrainingCourseService(IRepository<TbltrainingCourse> courserepo, IRepository<TbltrainingCourseFee> feerepo, IRepository<TbltrainingCourseTopic> coursetopicrepo, IRepository<TbltrainingTopic> topicepo, IRepository<TbltopicContent> contentrepo)
        {
            this.courserepo = courserepo;
            this.feerepo = feerepo;
            this.coursetopicrepo = coursetopicrepo;
            this.topicepo = topicepo;
            this.contentrepo = contentrepo;
        }
        public void AddCourse(TbltrainingCourse course)
        {
            courserepo.Create(course);
        }
        public void AddCourseFees(TbltrainingCourseFee course_fee)
        {
            feerepo.Create(course_fee);
        }

        public void AddCourseTopic(TbltrainingCourseTopic course_topic)
        {
            coursetopicrepo.Create(course_topic);

        }

        public void DeleteCourse(int course_id)
        {
            courserepo.Delete(course_id);
        }

        public void DeleteCourseFees(int course_id)
        {
            feerepo.Delete(course_id);
        }

        public void DeleteCourseTopic(int course_topic_id)
        {
            coursetopicrepo.Delete(course_topic_id);
        }

        public List<CourseModel> GetCourse(int course_id)
        {
             return GetCourses().Where(e=>e.CourseId.Equals(course_id)).ToList();
        }
        public  CourseModel  GetFeeIdWiseCourse(int fee_id)
        {
            return GetCourses().FirstOrDefault(e => e.FeeId.Equals(fee_id));
        }
        public List<CourseModel> GetCourses()
        {
            List<CourseModel> lst = new List<CourseModel>();
            var query = from c in courserepo.GetAll()
                        join f in feerepo.GetAll() on c.CourseId equals f.CourseId
                        select new {
                         course_id=c.CourseId,
                         course_name=c.CourseName,
                         fee_id=f.FeeId,
                         fee_mode=f.FeeMode,
                         fees_amount=f.FeesAmount,
                         gst=f.Gst
                        };
            foreach(var q in query)
            {
                CourseModel cm = new CourseModel() 
                {
                 CourseId=q.course_id,
                  Gst=(float)q.gst,
                   CourseName=q.course_name,
                    FeeAmount= (float)q.fees_amount,
                     FeeId=q.fee_id,
                      FeeMode=q.fee_mode,
                };
                lst.Add(cm);
            }
            return lst;
        }

        public List<TbltrainingCourse> GetAllCourseDetails()
        {
            List<TbltrainingCourse> lst = new List<TbltrainingCourse>();
            foreach(TbltrainingCourse c in courserepo.GetAll())
            {

                List<TbltrainingCourseFee> fees = feerepo.GetAll().Where(e => e.CourseId.Equals(c.CourseId)).ToList();
                c.TbltrainingCourseFees = fees;
                List<TbltrainingCourseTopic> topics = new List<TbltrainingCourseTopic>();
                foreach(TbltrainingCourseTopic tp in coursetopicrepo.GetAll().Where(e => e.CourseId.Equals(c.CourseId)).ToList())
                {
                    TbltrainingTopic p = topicepo.GetById((int)tp.TopicId);
                    List<TbltopicContent> contents=contentrepo.GetAll().Where(e => e.TopicId.Equals(p.TopicId)).ToList();
                    p.TbltopicContents = contents;
                    tp.Topic = p;
                    
                    topics.Add(tp);
                }
                c.TbltrainingCourseTopics = topics;
                lst.Add(c);
            }

            return lst;
        }

        public TopicCourseModel GetCourseTopic(int topic_course_id)
        {
            TopicCourseModel cm = GetCourseTopics().FirstOrDefault(e => e.TopicCourseId.Equals(topic_course_id));
            return cm;
        }

        public List<TopicCourseModel> GetCourseTopics()
        {
            List<TopicCourseModel> lst = new List<TopicCourseModel>();
            foreach(TbltrainingCourseTopic t in coursetopicrepo.GetAll())
            {
                TopicCourseModel cm = new TopicCourseModel() 
                {
                     TopicCourseId=t.CourseTopicId,
                CourseId=(int)t.CourseId,
                 CourseName=courserepo.GetById((int)t.CourseId).CourseName,
                  TopicId=(int)t.TopicId,
                   TopicName=topicepo.GetById((int)t.TopicId).TopicName
                };
                lst.Add(cm);
            }
            return lst;
        }

        public List<CourseModel> GetCourseWiseFees(int course_id)
        {
             return GetCourses().Where(e=>e.CourseId.Equals(course_id)).ToList();   
        }

        public List<TopicCourseModel> GetCourseWiseTopics(int course_id)
        {

            List<TopicCourseModel> lst = GetCourseTopics().Where(e => e.CourseId.Equals(course_id)).ToList();
            return lst;
        }

        public CourseModel GetFeeModeAndCourseIdWiseCourse(int course_id, string fee_mode)
        {
            return GetCourses().Where(e => e.FeeMode.Equals(fee_mode) & e.CourseId.Equals(course_id)).ToList().First();

        }

        public List<CourseModel> GetFeeModeWiseCourses(string fee_mode)
        {
            return GetCourses().Where(e => e.FeeMode.Equals(fee_mode)).ToList();
        }

        public List<TopicCourseModel> GetTopicWiseCourse(int topic_id)
        {
            List<TopicCourseModel> lst = GetCourseTopics().Where(e => e.TopicId.Equals(topic_id)).ToList();
            return lst;
        }

        public TbltrainingCourse GetCourseWiseDetails(int course_id)
        {
            return GetAllCourseAllDetails().FirstOrDefault(e => e.CourseId.Equals(course_id));
        }
        public List<TbltrainingCourse> GetAllCourseAllDetails()
        {



            List<TbltrainingCourse> lst = new List<TbltrainingCourse>();
            foreach(TbltrainingCourse c in courserepo.GetAll())
            {
               
                List<TbltrainingCourseTopic> topiclist = new List<TbltrainingCourseTopic>();
                List<TbltrainingCourseTopic> coursetopics = coursetopicrepo.GetAll().Where(e => e.CourseId.Equals(c.CourseId)).ToList();
                foreach(TbltrainingCourseTopic t in coursetopics)
                {
                    TbltrainingTopic tp = topicepo.GetById((int)t.TopicId);
                    t.Topic = tp;
                    topiclist.Add(t);
                }

                c.TbltrainingCourseTopics=topiclist;
                lst.Add(c);
            }
            return lst;
        }

        public void UpdateCourse(TbltrainingCourse course)
        {

            courserepo.Update(course);
        }

        public void UpdateCourseFees(TbltrainingCourseFee course_fee)
        {
            feerepo.Update(course_fee);
        }

        public void UpdateCourseTopic(TbltrainingCourseTopic course_topic)
        {
            coursetopicrepo.Update(course_topic);
        }
    }
}
