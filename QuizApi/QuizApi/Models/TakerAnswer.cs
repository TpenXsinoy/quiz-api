using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace QuizApi.Models
{
    public class TakerAnswer
    {
        /// <summary>
        /// Properties for TakerAnswer
        /// </summary>
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public string? Status { get; set; }
    }
}
