namespace QuizApi.Dtos.QuizResult
{
    public class QuizResultsForTakerDto
    {
        public int Id { get; set; }
        public string? QuizName { get; set; }
        public int Score { get; set; }
        public string? Evaluation { get; set; }
    }
}
