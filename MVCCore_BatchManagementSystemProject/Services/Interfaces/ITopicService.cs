using MVCCore_BatchManagementSystemProject.Models;

namespace MVCCore_BatchManagementSystemProject.Services.Interfaces
{
    public interface ITopicService
    {
        void AddTopic(TbltrainingTopic topic);
        void UpdateTopic(TbltrainingTopic topic);
        void DeleteTopic(int topic_id);
        List<TbltrainingTopic> GetTopics();
        List<TopicCourseModel> GetCourseWiseTopics(int course_id);
        TbltrainingTopic GetTopic(int topic_id);


    }
}
