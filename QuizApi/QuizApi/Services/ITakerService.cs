using QuizApi.Dtos.Taker;

namespace QuizApi.Services
{
    public interface ITakerService
    {
        /// <summary>
        /// Creates <param name="takerToCreate">TakerCreationDto Details</param>
        /// </summary>
        /// <returns>TakerDto deatils</returns>
        Task<TakerDto> CreateTaker(TakerCreationDto takerToCreate);

        /// <summary>
        /// Get all takers
        /// </summary>
        /// <returns>All takers with TakerDto details</returns>
        Task<IEnumerable<TakerDto>> GetAllTakers();

        /// <summary>
        /// Get all takers by quiz Id = <param name="quizId">Quiz id</param>
        /// </summary>
        /// <returns>All Takers with TakerQuizDto details with a quiz</returns>
        Task<IEnumerable<TakerQuizDto>> GetAllTakers(int quizId);

        /// <summary>
        /// Gets taker with id = <param name="id">Id of Taker</param>
        /// </summary>
        /// <returns>Taker with TakerQuizzesDto details</returns>
        Task<TakerQuizzesDto?> GetTakerById(int id);

        /// <summary>
        /// Gets taker with quiz by id = <param name="id">Id of Taker</param>
        /// </summary>
        /// <returns>Taker with TakerQuizzesDto details</returns>
        Task<TakerQuizzesDto?> GetTakerWithQuizById(int id);

        /// <summary>
        /// Get taker with quiz results by id = <param name="id">Id of taker</param>
        /// </summary>
        /// <returns>Taker with TakerQuizzesDto details</returns>
        Task<TakerQuizResultDto?> GetTakerWithQuizResultById(int id);

        /// <summary>
        /// Get taker answer by id = <param name="id">Id of Taker</param>
        /// </summary>
        /// <returns>Taker's answer to a question with TakerAnswersDto details</returns>
        Task<TakerAnswersDto?> GetTakerAnswersById(int id);

        /// <summary>
        /// Updates taker <param name="id">Taker to update</param>
        /// </summary>
        /// <param name="takerToUpdate">Taker Details</param>
        /// <returns>Newly updated Taker with TakerDto details</returns>
        Task<TakerDto> UpdateTaker(int id, TakerCreationDto takerToUpdate);

        /// <summary>
        /// Deletes taker with id <param name="id">Id of taker</param>
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