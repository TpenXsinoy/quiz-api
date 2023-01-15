using QuizApi.Dtos.QuizResult;
using QuizApi.Dtos.Taker;

namespace QuizApi.Dtos.QuizD
{
    public class QuizQuizResultDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<QuizResultsForQuiz> QuizResults { get; set; } = new List<QuizResultsForQuiz>();
    }
}
