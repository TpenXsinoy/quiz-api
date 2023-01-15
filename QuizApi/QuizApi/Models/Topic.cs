namespace QuizApi.Models
{
    public class Topic
    {
        /// <summary>
        /// Properties for Topic
        /// </summary>
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Problem> Questions { get; set; } = new List<Problem>();
    }
}
