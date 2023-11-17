using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;

namespace MVCCore_BatchManagementSystemProject.Services.Implementations
{
    public class TopicContentService : ITopicContentService
    {

        IRepository<TbltrainingTopic> topicrepo;
        IRepository<TbltopicContent> contentrepo;
        public TopicContentService(IRepository<TbltrainingTopic> topicrepo, IRepository<TbltopicContent> contentrepo)
        {
            this.topicrepo = topicrepo;
            this.contentrepo = contentrepo;
        }

        public void AddTopicContent(TbltopicContent content)
        {
            contentrepo.Create(content);
        }

        public void DeleteTopicContent(int content_id)
        {
            contentrepo.Delete(content_id);
        }

        public TopicContentModel GetTopicContent(int content_id)
        {
            return GetTopicContents().FirstOrDefault(e => e.ContentId.Equals(content_id));
        }

        public List<TopicContentModel> GetTopicContents()
        {
            List<TopicContentModel> lst = new List<TopicContentModel>();
            var query = from t in topicrepo.GetAll()
                        join c in contentrepo.GetAll() on t.TopicId equals c.TopicId
                        select new { 
                            topic_id=t.TopicId,
                            topic_name=t.TopicName,
                            content_id=c.ContentId,
                            content_name=c.ContentName
                        };
            foreach(var q in query)
            {
                TopicContentModel tc = new TopicContentModel() 
                {
                 ContentId=q.content_id,
                  ContentName=q.content_name,
                   TopicId=q.topic_id,
                    TopicName=q.topic_name
                };
                lst.Add(tc);
            }
            return lst;
        }

        public List<TopicContentModel> GetTopicWiseContents(int topic_id)
        {

            return GetTopicContents().Where(e => e.TopicId.Equals(topic_id)).ToList();
        }

        public void UpdateTopicContent(TbltopicContent content)
        {
            throw new NotImplementedException();
        }



    }
}
