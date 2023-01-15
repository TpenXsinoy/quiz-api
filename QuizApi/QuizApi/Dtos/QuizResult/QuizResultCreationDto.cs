using System.ComponentModel.DataAnnotations;
using QuizApi.Models;

namespace QuizApi.Dtos.QuizResult
{
    public class QuizResultCreationDto
    {
        [Required(ErrorMessage = "Quiz name is required.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Quiz name is 50 characters.")]
        public string? QuizName { get; set; }
        
        [Required(ErrorMessage = "Taker name is required.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Taker name is 50 characters.")]
        [RegularExpression("^([a-zA-Z]{2,}\\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\\s?([a-zA-Z]{1,})?)", ErrorMessage = "Valid Characters include (A-Z) (a-z) (' space -)")]
        public string? TakerName { get; set; }
    }
}
