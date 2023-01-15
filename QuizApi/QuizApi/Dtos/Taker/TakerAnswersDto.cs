using QuizApi.Models;

namespace QuizApi.Dtos.Taker
{
    public class TakerAnswersDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public List<TakerAnswer> TakerAnswers { get; set; } = new List<TakerAnswer>();
    }
}
