using QuizApi.Dtos.QuizD;
using QuizApi.Models;

namespace QuizApi.Services
{
    /// <summary>
    /// Interface for Quiz service
    /// </summary>
    public interface IQuizService
    {
        /// <summary>
        /// Calls QuizRepository to create a Quiz.
        /// </summary>
        /// <param name="quizToCreate">Quiz Details</param>
        /// <returns>Id of newly created quiz</returns>
        Task<QuizDto> CreateQuiz(QuizCreationDto quizToCreate);

        /// <summary>
        /// Calls QuizRepository to get quiz by id. 
        /// </summary>
        /// <param name="id">id of quiz</param>
        /// <returns>QuizDto</returns>
        Task<QuizDto?> GetQuizById(int id);

        /// <summary>
        /// Calls QuizRepository to get quiz by id with topics. 
        /// </summary>
        /// <param name="id">id of quiz</param>
        /// <returns>QuizTopicsDto</returns>
        Task<QuizTopicsDto?> GetQuizByIdWithTopics(int id);

        /// <summary>
        ///  Calls QuizRepository to get quiz by id with takers       
        /// </summary>
        /// <param name="id">Id of quiz</param>
        /// <returns>QuizTakersDto with takers</returns>
        Task<QuizTakersDto?> GetQuizByIdWithTakers(int id);

        /// <summary>
        /// Calls QuizRepository to get quiz with quiz results by id.
        /// </summary>
        /// <param name="id">Id of quiz</param>
        /// <returns>QuizTakersDto with quiz results</returns>
        Task<QuizQuizResultDto?> GetQuizByIdWithQuizResults(int id);

        /// <summary>
        /// Calls QuizRepository to get all Quizzes
        /// </summary>
        /// <returns>All Quizzes with topics</returns>
        Task<IEnumerable<QuizDto>> GetAllQuizzes();

        /// <summary>
        /// Calls QuizRepository to get all Quiz by topic Id.
        /// </summary>
        /// <param name="id">Topic id</param>
        /// <returns>All Quiz with topics given TopicId</returns>
        Task<IEnumerable<QuizTopicDto>> GetAllQuizzesByTopicId(int id);

        /// <summary>
        /// Calls QuizRepository to get all Quiz by taker Id.
        /// </summary>
        /// <param name="id">Taker id</param>
        /// <returns>All Quiz that a taker is assigned to given TakerId</returns>
        Task<IEnumerable<QuizTakerDto>> GetAllQuizzesByTakerId(int id);

        /// <summary>
        /// Calls QuizRepository to update a quiz given its id.
        /// </summary>
        /// <param name="id">Id of Quiz to update</param>
        /// <param name="quizToUpdate">Quiz to update</param>
        /// <returns>QuizDto of the updatd quiz</returns>
        Task<QuizDto> UpdateQuiz(int id, QuizCreationDto quizToUpdate);

        /// <summary>
        /// Calls QuizRepository to delete quiz with id
        /// and deletes connection between quiz and taker.
        /// </summary>
        /// <param name="id">Id of quiz to delete</param>
        /// <returns>True if delete is successful, otherwise false</returns>
        Task<bool> DeleteQuiz(int id);

        /// <summary>
        /// Calls QuizRepository to check quiz if it exists given its id.
        /// </summary>
        /// <param name="id">Id of quiz to check</param>
        /// <returns>True if quiz is exists, otherwise false</returns>
        Task<bool> CheckQuizById(int id);
    }
}
