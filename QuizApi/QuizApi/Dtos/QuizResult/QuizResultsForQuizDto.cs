namespace QuizApi.Dtos.QuizResult
{
    public class QuizResultsForQuiz
    {
        public int Id { get; set; }
        public string? TakerName { get; set; }
        public int Score { get; set; }
        public string? Evaluation { get; set; }
    }
}
