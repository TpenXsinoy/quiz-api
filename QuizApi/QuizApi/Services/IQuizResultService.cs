using QuizApi.Dtos.QuizResult;
using QuizApi.Models;

namespace QuizApi.Services
{
    /// <summary>
    /// Interface for Quiz result service
    /// </summary>
    public interface IQuizResultService
    {
        /// <summary>
        /// Creates <param name="quizresultToCreate">QuizResultCreationDto details</param>
        /// </summary>
        /// <returns>QuizResultCreationDto details</returns>
        Task<QuizResultDto> CreateQuizResult(QuizResultCreationDto quizresultToCreate);

        /// <summary>
        /// Get all quiz results
        /// </summary>
        /// <returns>All quiz results with QuizResultDto details</returns>
        Task<IEnumerable<QuizResultDto>> GetAllQuizResult();

        /// <summary>
        /// Get quiz results by quiz Id = <param name="id">Quiz id</param>
        /// </summary>
        /// <returns>Quiz result with the given id and QuizResultDto details</returns>
        Task<QuizResultDto?> GetQuizResultById(int id);

        /// <summary>
        /// Updates quiz result <param name="id"> Id of Quiz result to update</param>
        /// </summary>
        /// <param name="quizResultToUpdate">Quiz result details</param>
        /// <returns>Newly updated Quiz result with QuizResultDto details</returns>
        Task<QuizResultDto> UpdateQuizResult(int id, QuizResultUpdateDto quizResultToUpdate);

        /// <summary>
        /// Deletes quiz result with id <param name="id">Id of quiz result</param>
        /// </summary>
        /// <returns>True if delete is successful, otherwise false</returns>
        Task<bool> DeleteQuizResult(int id);
    }
}
