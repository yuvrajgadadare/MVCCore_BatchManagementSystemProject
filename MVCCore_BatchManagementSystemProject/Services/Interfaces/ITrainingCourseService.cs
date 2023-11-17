using MVCCore_BatchManagementSystemProject.Models;

namespace MVCCore_BatchManagementSystemProject.Services.Interfaces
{
    public interface ITrainingCourseService
    {
        void AddCourse(TbltrainingCourse course);
        void UpdateCourse(TbltrainingCourse course);
        void DeleteCourse(int course_id);
        List<TbltrainingCourse> GetAllCourseDetails();
        List<CourseModel> GetCourse(int course_id);
        CourseModel GetFeeModeAndCourseIdWiseCourse(int course_id, string fee_mode);
        void AddCourseFees(TbltrainingCourseFee course_fee);
        void UpdateCourseFees(TbltrainingCourseFee course_fee);
        void DeleteCourseFees(int course_id);
        List<CourseModel> GetCourses();
        List<CourseModel>GetCourseWiseFees(int course_id);
        void AddCourseTopic(TbltrainingCourseTopic course_topic);
        void UpdateCourseTopic(TbltrainingCourseTopic course_topic);
        void DeleteCourseTopic(int course_topic_id);
        List<TopicCourseModel> GetCourseTopics();
        List<TopicCourseModel> GetCourseWiseTopics(int course_id);
        TopicCourseModel GetCourseTopic(int topic_course_id);
        List<TopicCourseModel> GetTopicWiseCourse(int topic_id);
        CourseModel GetFeeIdWiseCourse(int fee_id);
    }
}
