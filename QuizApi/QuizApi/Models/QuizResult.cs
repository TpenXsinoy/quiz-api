namespace QuizApi.Models
{
    public class QuizResult
    {
        /// <summary>
        /// Properties for QuizResult
        /// </summary>
        public int Id { get; set; } 
        public string? QuizName { get; set; }
        public string? TakerName { get; set; }
        public int Score { get; set; }
        public string? Evaluation { get; set; }
        
    }
}
