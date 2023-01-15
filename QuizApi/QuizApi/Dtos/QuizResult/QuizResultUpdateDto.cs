using System.ComponentModel.DataAnnotations;

namespace QuizApi.Dtos.QuizResult
{
    public class QuizResultUpdateDto
    {
        [Required(ErrorMessage = "Score is required.")]
        public int Score { get; set; }
        
        [Required(ErrorMessage = "Evaluation is required.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Quiz result's evaluation is 50 characters.")]
        public string? Evaluation { get; set; }
    }
}
