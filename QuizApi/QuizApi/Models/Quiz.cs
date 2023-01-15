using Microsoft.Identity.Client;
using QuizApi.Dtos.QuizResult;
using QuizApi.Dtos.Taker;
using QuizApi.Dtos.Topic;
using System.Globalization;

namespace QuizApi.Models
{
    public class Quiz
    {
        /// <summary>
        /// Properties for Quiz
        /// </summary>
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<TopicDto> Topics { get; set; } = new List<TopicDto>();
        public List<TakerDto> Takers { get; set; } = new List<TakerDto>();
        public List<QuizResultsForQuiz> QuizResults { get; set; } = new List<QuizResultsForQuiz>();
    }
}
