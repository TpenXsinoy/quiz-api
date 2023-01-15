using QuizApi.Context;
using QuizApi.Models;
using Dapper;

namespace QuizApi.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        /// <summary>
        /// Stores db server connection string initialized in the constructor to map objects
        /// </summary>
        private readonly DapperContext _context;

        /// <summary>
        /// Constructor where _context is initialized with db server connection string
        /// </summary>
        public QuestionRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<int> CreateQuestion(Problem question)
        {
            var sql = "INSERT INTO Questions (TopicId, Question, CorrectAnswer) VALUES (@TopicId, @Question, @CorrectAnswer) " +
                "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { question.TopicId, question.Question, question.CorrectAnswer });
            }
        }

        public async Task<IEnumerable<Problem>> GetAllQuestions()
        {
            var sql = "SELECT * FROM Questions";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<Problem>(sql);
            }
        }
        public async Task<IEnumerable<Problem>> GetAllQuestionsByTopicId(int id)
        {
            var sql = "SELECT * FROM Questions WHERE TopicId = @id";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<Problem>(sql, new { id });
            }
        }

        public async Task<Problem> GetQuestionById(int id)
        {
            var sql = "SELECT * FROM Questions WHERE Id = @id";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<Problem>(sql, new { id });
            }
        }

        public async Task<bool> UpdateQuestion(Problem question)
        {
            var sql = "UPDATE Questions SET Question = @Question, CorrectAnswer = @CorrectAnswer WHERE Id = @Id;";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(sql, new { question.Id, question.Question, question.CorrectAnswer }) > 0;
            }
        }
        public async Task<bool> DeleteQuestion(int id)
        {
            var sql = "DELETE FROM TakersAnswers WHERE QuestionId = @Id " +
                "DELETE FROM Questions WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                int rowAffected = await con.ExecuteAsync(sql, new { id });
                return rowAffected > 0 ? true : false;
            }
        }
    }
}
