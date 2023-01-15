using QuizApi.Dtos.Taker;
using QuizApi.Dtos.Topic;
using QuizApi.Models;

namespace QuizApi.Services
{
    public interface ITopicService
    {
        /// <summary>
        /// Create topic
        /// </summary>
        /// <param name="topicToCreate">Topic details</param>
        /// <param name="quizId">Id of quiz</param>
        /// <returns>Newly created topic with TopicDto details</returns>
        Task<TopicDto> CreateTopic(TopicCreationDto topicToCreate, int quizId);

        /// <summary>
        /// Get all topics
        /// </summary>
        /// <returns>All topic with TopicDto details</returns>
        Task<IEnumerable<TopicDto>> GetAllTopics();

        /// <summary>
        /// Get all topics
        /// </summary>
        /// <param name="quizId">Id of quiz</param>
        /// <returns>All topics with quiz id</returns>
        Task<IEnumerable<TopicQuizDto>> GetAllTopics(int quizId);

        /// <summary>
        /// Get topic by Id
        /// </summary>
        /// <param name="id">Id of the topic</param>
        /// <returns>Topic with TopicDto details</returns>
        Task<TopicDto?> GetTopicById(int id);

        /// <summary>
        /// Get topic with questions
        /// </summary>
        /// <param name="id">Id of topic</param>
        /// <returns>Topic with questions</returns>
        Task<Topic?> GetTopicWithQuestion(int id);

        /// <summary>
        /// Update topic
        /// </summary>
        /// <param name="id">Id of topic</param>
        /// <param name="topicToUpdate">Topic to be updated</param>
        /// <returns>newly updated topic with TopicDto details</returns>
        Task<TopicDto> UpdateTopic(int id, TopicCreationDto topicToUpdate);

        /// <summary>
        /// Delete topic
        /// </summary>
        /// <param name="id">Id of topic</param>
        /// <returns>True if delete is successful, otherwise false</returns>
        Task<bool> DeleteTopic(int id);
    }
}