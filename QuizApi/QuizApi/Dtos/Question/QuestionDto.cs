namespace QuizApi.Dtos.Question
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string? Question { get; set; }
        public string? CorrectAnswer { get; set; }
    }
}
