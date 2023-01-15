using QuizApi.Models;

namespace QuizApi.Repositories
{
    /// <summary>
    /// Interface for taker repository
    /// </summary>
    public interface ITakerRepository
    {
        /// <summary>
        /// Creates <param name="taker">Taker Details</param>
        /// </summary>
        /// <returns>Id of newly created taker</returns>
        Task<int> CreateTaker(Taker taker);

        /// <summary>
        /// Get all takers
        /// </summary>
        /// <returns>All takers with quizzes</returns>
        Task<IEnumerable<Taker>> GetAll();

        /// <summary>
        /// Get all takers by quiz Id = <param name="quizId">Quiz id</param>
        /// </summary>
        /// <returns>All takers with a quiz given quizId</returns>
        Task<IEnumerable<Taker>> GetAllByQuizId(int quizId);

        /// <summary>
        /// Gets taker with id = <param name="id">Id of Taker</param>
        /// </summary>
        /// <returns>Taker</returns>
        Task<Taker?> GetTaker(int id);

        /// <summary>
        /// Gets taker with quiz by id = <param name="id">Id of Taker</param>
        /// </summary>
        /// <returns>Taker with quizzes</returns>
        Task<Taker?> GetTakerWithQuiz(int id);

        /// <summary>
        /// Get taker with quiz results by id = <param name="id">Id of taker</param>
        /// </summary>
        /// <returns>Taker with quiz results</returns>
        Task<Taker?> GetTakerWithQuizResult(int id);

        /// <summary>
        /// Get taker answer by id = <param name="id">Id of Taker</param>
        /// </summary>
        /// <returns>Taker's answer to a question</returns>
        Task<Taker?> GetTakerAnswers(int id);

        /// <summary>
        /// Updates taker <param name="taker">Taker to update</param>
        /// </summary>
        /// <returns>True if update is successful, otherwise false</returns>
        Task<bool> UpdateTaker(Taker taker);

        /// <summary>
        /// Deletes taker with id <param name="id">Id of taker to delete</param>
        /// and Deletes connection between TakerQuiz, TakersAnswers, and TakerQuizResults
        /// </summary>
        /// <returns>True if delete is successful, otherwise false</returns>
        Task<bool> DeleteTaker(int id);

        /// <summary>
        /// Lets taker take a quiz
        /// </summary>
        /// <param name="takerId">Id of Taker</param>
        /// <param name="quizId">Id of a Quiz</param>
        /// <returns>Id of the connection between Taker and Quiz </returns>
        Task<int> LetTakerTakeQuiz(int takerId, int quizId);

        /// <summary>
        /// Checks if Taker has already taken quiz with id = <param name="quizId">Id of a Quiz</param>
        /// </summary>
        /// <param name="takerId">Id of Taker</param>
        /// <returns>True if Taker has taken quiz, otherwise false</returns>
        Task<bool> HasTakerTakenQuiz(int takerId, int quizId);
    }
}
