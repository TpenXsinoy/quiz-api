using QuizApi.Dtos.QuizD;

namespace QuizApi.Dtos.Taker
{
    public class TakerQuizzesDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public List<QuizDto> Quizzes { get; set; } = new List<QuizDto>();  
    }
}
