using QuizApi.Dtos.QuizResult;

namespace QuizApi.Dtos.Taker
{
    public class TakerQuizResultDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public List<QuizResultsForTakerDto> QuizResults { get; set; } = new List<QuizResultsForTakerDto>();
    }
}
