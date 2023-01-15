using QuizApi.Models;

namespace QuizApi.Repositories
{
    /// <summary>
    /// Interface for quiz result repository
    /// </summary>
    public interface IQuizResultRepository
    {
        /// <summary>
        /// Creates <param name="quizResult">Quiz Result Details</param>
        /// </summary>
        /// <returns>Id of the newly created Quiz Result</returns>
        Task<int> CreateQuizResult(QuizResult quizResult);

        /// <summary>
        /// Get all quiz results
        /// </summary>
        /// <returns> All Quiz Results of all takers</returns>
        Task<IEnumerable<QuizResult>> GetAllQuizResult();

        /// <summary>
        /// Get all quiz results by id = <param name="id">QuizResult id</param>
        /// </summary>
        /// <returns>Quiz result with the given id</returns>
        Task<QuizResult> GetQuizResultById(int id);

        /// <summary>
        /// Updates quiz result details <param name="quizResult">Quiz Result to update</param>
        /// </summary>
        /// <returns>True if update is successful, otherwise false</returns>
        Task<bool> UpdateQuizResult(QuizResult quizResult);

        /// <summary>
        /// Deletes quiz result with id <param name="id">Id of quiz result to delete</param>
        /// </summary>
        /// <returns>True if update is successful, otherwise false</returns>
        Task<bool> DeleteQuizResult(int id);

        /// <summary>
        /// Checks if the taker has already answered the quiz
        /// </summary>
        /// <returns>Number of answers Taker has, if 0 = Taker has not answered any questions</returns>
        Task<int> CheckTakerHasAnswers(string? quizName, string? takerName);
    }
}
