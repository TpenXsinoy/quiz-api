using QuizApi.Models;

namespace QuizApi.Repositories
{
    /// <summary>
    /// Interface for Quiz repository
    /// </summary>
    public interface IQuizRepository
    {
        /// <summary>
        /// Creates a Quiz.
        /// </summary>
        /// <param name="quiz">Quiz Details</param>
        /// <returns>Id of newly created quiz</returns>
        Task<int> CreateQuiz(Quiz quiz);

        /// <summary>
        /// Gets quiz with id. 
        /// </summary>
        /// <param name="id">Id of quiz</param>
        /// <returns>Quiz given its Id</returns>
        Task<Quiz?> GetQuiz(int id);

        /// <summary>
        /// Gets quiz by id with topics. 
        /// </summary>
        /// <param name="id">Id of quiz</param>
        /// <returns>quiz with its topics</returns>
        Task<Quiz?> GetQuizByIdWithTopics(int id);

        /// <summary>
        /// Gets quiz by id with takers       
        /// </summary>
        /// <param name="id">Id of quiz</param>
        /// <returns>Quiz with takers</returns>
        Task<Quiz?> GetQuizWithTakers(int id);

        /// <summary>
        /// Get all Quizzes
        /// </summary>
        /// <returns>All Quizzes with topics</returns>
        Task<IEnumerable<Quiz>> GetAllQuiz();

        /// <summary>
        /// Get all Quiz by topic Id.
        /// </summary>
        /// <param name="id">Topic id</param>
        /// <returns>All Quiz that a topic is assigned to given TopicId</returns>
        Task<IEnumerable<Quiz>> GetAllQuizByTopicId(int id);

        /// <summary>
        /// Get all Quiz by taker Id.
        /// </summary>
        /// <param name="id">Taker id</param>
        /// <returns>All Quiz that a taker is assigned to given TakerId</returns>
        Task<IEnumerable<Quiz>> GetAllQuizByTakerId(int id);

        /// <summary>
        /// Get quiz with quiz results by id.
        /// </summary>
        /// <param name="id">Id of quiz</param>
        /// <returns>Quiz with quiz results</returns>
        Task<Quiz?> GetQuizWithQuizResults(int id);

        /// <summary>
        /// Updates a quiz given its id, also updates other rows that references
        /// the given Quiz.
        /// </summary>
        /// <param name="quiz">Quiz to update</param>
        /// <returns>True if update is successful, otherwise false</returns>
        Task<bool>UpdateQuiz(Quiz quiz);

        /// <summary>
        /// Deletes quiz with id
        /// and deletes connection between quiz and taker.
        /// </summary>
        /// <param name="id">Id of quiz to delete</param>
        /// <returns>True if delete is successful, otherwise false</returns>
        Task<bool>DeleteQuiz(int id);

        /// <summary>
        /// Checks quiz if it exists given its id.
        /// </summary>
        /// <param name="id">Id of quiz to check</param>
        /// <returns>True if quiz is exists, otherwise false</returns>
        Task<bool> CheckQuizId(int id);
    }
}
