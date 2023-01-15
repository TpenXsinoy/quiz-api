namespace QuizApi.Models
{
    public class Problem
    {
        /// <summary>
        /// Properties for Problem (Questions table)
        /// </summary>
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string? Question { get; set; }
        public string? CorrectAnswer { get; set; }
    }
}
