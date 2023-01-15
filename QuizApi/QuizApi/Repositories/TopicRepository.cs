using Dapper;
using QuizApi.Context;
using QuizApi.Dtos.QuizD;
using QuizApi.Dtos.Topic;
using QuizApi.Models;
using System.Data;

namespace QuizApi.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        /// <summary>
        /// Stores db server connection string initialized in the constructor to map objects
        /// </summary>
        private readonly DapperContext _context;

        /// <summary>
        /// Constructor where _context is initialized with db server connection string
        /// </summary>
        public TopicRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateTopic(Topic topic, int quizId)
        {
            var sql = "INSERT INTO Topics (QuizId, Name) VALUES (@QuizId, @Name); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { QuizId = quizId, Name = topic.Name });
            }
        }

        public async Task<IEnumerable<Topic>> GetAllTopics()
        {
            var sql = "SELECT t.Id, t.Name, q.Id, q.Question, q.CorrectAnswer FROM Topics t " +
                        "INNER JOIN Questions q ON q.TopicId = t.Id ";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Topic, Problem, Topic>(sql, MapTopicQuestions);

                return result.GroupBy(s => s.Id).Select(TopicGroup =>
                {
                    var firstTopic = TopicGroup.First();
                    firstTopic.Questions = TopicGroup.SelectMany(topic => topic.Questions).ToList();
                    return firstTopic;
                });
            }
        }

        public async Task<Topic?> GetTopicWithQuestion(int id)
        {
            var sql = "SELECT * FROM Topics t " +
                "INNER JOIN Questions q ON q.TopicId = t.Id " +
                "WHERE t.Id = @Id;";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Topic, Problem, Topic>(
                    sql, 
                    MapTopicQuestions, 
                    new { id });

                return result.GroupBy(t => t.Id).Select(TopicGroup =>
                {
                    var firstTopic = TopicGroup.First();
                    firstTopic.Questions = TopicGroup.SelectMany(topic => topic.Questions).ToList();
                    return firstTopic;
                }).SingleOrDefault();
            }
        }

        public async Task<Topic?> GetTopic(int id)
        {
            var sql = "SELECT t.Id, t.Name FROM Topics t WHERE  t.Id = @Id;";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Topic>(sql, new { id });
            }
        }

        public async Task<IEnumerable<TopicQuizDto>> GetAllTopicsByQuizId(int quizId)
        {
            var sql = "[spTopic_GetAllTopicsByQuizId]";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<TopicQuizDto, Quiz, TopicQuizDto>(
                     sql,
                     (topicQuiz, quiz) =>
                     {
                         topicQuiz.QuizName = quiz.Name;
                         return topicQuiz;
                     }, new { quizId },
                     commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> UpdateTopic(Topic topic)
        {
            var sql = "UPDATE Topics SET Name = @Name WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(sql, new { Id = topic.Id, Name = topic.Name });
                return rowsAffected != 0;
            }
        }

        public async Task<bool> DeleteTopic(int id)
        {
            var sql = "[spTopic_DeleteTopic]";

            using (var connection = _context.CreateConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(
                    sql, 
                    new { id }, 
                    commandType: CommandType.StoredProcedure);
                return rowsAffected != 0;
            }
        }

        private static Topic MapTopicQuestions(Topic topic, Problem question)
        {
            topic.Questions.Add(question);
            return topic;
        }
    }
}
