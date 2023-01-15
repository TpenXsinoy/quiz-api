using QuizApi.Context;
using QuizApi.Dtos.Topic;
using QuizApi.Models;

namespace QuizApi.Repositories
{
    /// <summary>
    /// Interface for topic repository
    /// </summary>
    public interface ITopicRepository
    {
        /// <summary>
        /// Creates topics
        /// </summary>
        /// <param name="topic">Topic details</param>
        /// <param name="quizId">Id of Quiz</param>
        /// <returns>The newly created topic and quizId</returns>
        Task<int> CreateTopic(Topic topic, int quizId);

        /// <summary>
        /// Get all topics
        /// </summary>
        /// <returns>All topics</returns>
        Task<IEnumerable<Topic>> GetAllTopics();

        /// <summary>
        /// Get all topics by quiz id
        /// </summary>
        /// <param name="quizId">Id of Quiz</param>
        /// <returns>All topics with quiz id</returns>
        Task<IEnumerable<TopicQuizDto>> GetAllTopicsByQuizId(int quizId);

        /// <summary>
        /// Get topics
        /// </summary>
        /// <param name="id">Topic details</param>
        /// <returns>Topic</returns>
        Task<Topic?> GetTopic(int id);

        /// <summary>
        /// Get topic with questions
        /// </summary>
        /// <param name="id">Id of topic Id</param>
        /// <returns>Topic with questions</returns>
        Task<Topic?> GetTopicWithQuestion(int id);

        /// <summary>
        /// Updates topics
        /// </summary>
        /// <param name="topic">Topic details</param>
        /// <returns>The newly updated topics</returns>
        Task<bool> UpdateTopic(Topic topic);

        /// <summary>
        /// Deletes topics
        /// </summary>
        /// <param name="id">DeleteTopic Id</param>
        /// <returns>True if deletion successful otherwise false </returns>
        Task<bool> DeleteTopic(int id);
    }
}
