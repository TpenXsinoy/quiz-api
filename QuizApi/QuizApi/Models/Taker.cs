using QuizApi.Dtos.QuizD;
using QuizApi.Dtos.QuizResult;

namespace QuizApi.Models
{
    public class Taker
    {
        /// <summary>
        /// Properties for Taker
        /// </summary>
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public List<QuizDto> Quizzes { get; set; } = new List<QuizDto>();
        public List<TakerAnswer> TakerAnswers { get; set; } = new List<TakerAnswer>();
        public List<QuizResultsForTakerDto> QuizResults { get; set; } = new List<QuizResultsForTakerDto>();
    }
}
