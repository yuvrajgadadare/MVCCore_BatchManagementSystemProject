using MVCCore_BatchManagementSystemProject.Models;

namespace MVCCore_BatchManagementSystemProject.Services.Interfaces
{
    public interface ITopicContentService
    {
        void AddTopicContent(TbltopicContent content);
        void UpdateTopicContent(TbltopicContent content);
        void DeleteTopicContent(int content_id);
        List<TopicContentModel> GetTopicContents();
        List<TopicContentModel> GetTopicWiseContents(int topic_id);
        TopicContentModel GetTopicContent(int content_id);

    }
}
