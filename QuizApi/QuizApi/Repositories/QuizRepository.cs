using Dapper;
using QuizApi.Context;
using QuizApi.Dtos.QuizResult;
using QuizApi.Dtos.Taker;
using QuizApi.Dtos.Topic;
using QuizApi.Models;
using System.Data;

namespace QuizApi.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        /// <summary>
        /// Stores db server connection string initialized in the constructor to map objects
        /// </summary>
        private readonly DapperContext _context;

        /// <summary>
        /// Constructor where _context is initialized with db server connection string
        /// </summary>
        public QuizRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateQuiz(Quiz quiz)
        {
            var sql = "INSERT INTO Quizzes (Name, Description) VALUES (@Name, @Description); " +
                "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { quiz.Name, quiz.Description });
            }
        }

        public async Task<Quiz?> GetQuiz(int id)
        {
            var sql = "SELECT * From Quizzes WHERE id = @id;";

            using (var con = _context.CreateConnection())
            {
                 return await con.QuerySingleOrDefaultAsync<Quiz>(sql, new { id });
            }
        }

        public async Task<Quiz?> GetQuizByIdWithTopics(int id)
        {
            var sp = "spQuiz_GetQuizById";

            using (var con = _context.CreateConnection())
            {
                var result = await con.QueryAsync<Quiz, TopicDto, Quiz>(
                    sp, 
                    MapQuizTopic, 
                    new { id }, 
                    commandType: CommandType.StoredProcedure);

                return result.GroupBy(s => s.Id).Select(QuizGroup =>
                {
                    var firstQuiz = QuizGroup.First();
                    firstQuiz.Topics = QuizGroup.SelectMany(quiz => quiz.Topics).ToList();
                    return firstQuiz;
                }).SingleOrDefault();
            }
        }

        public async Task<Quiz?> GetQuizWithTakers(int id)
        {
            var sp = "[spQuiz_GetQuizWithTakers]";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync(sp,
                    (Func<Quiz, TakerDto, Quiz>)((quiz, taker) =>
                    { 
                        quiz.Takers.Add(taker);
                        return quiz;
                    }), new { id }, commandType: CommandType.StoredProcedure);

                return result.GroupBy(s => s.Id).Select(QuizGroup =>
                {
                    var firstQuiz = QuizGroup.First();
                    firstQuiz.Takers = QuizGroup.SelectMany(quiz => quiz.Takers).GroupBy(t => t.Id).Select(taker =>
                    {
                        var firstTaker = taker.First();
                        return firstTaker;
                    }).ToList();
                    return firstQuiz;
                }).SingleOrDefault();
            }
        }

        public async Task<Quiz?> GetQuizWithQuizResults(int id)
        {
            var sp = "[spQuiz_GetQuizByIdWithQuizResults]";
            
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Quiz, QuizResultsForQuiz, Quiz>(sp,
                    (quiz, qr) =>
                    {
                        quiz.QuizResults.Add(qr);
                        return quiz;
                    }, new { id }, commandType: CommandType.StoredProcedure);

                return result.GroupBy(s => s.Id).Select(QuizGroup =>
                {
                    var firstQuiz = QuizGroup.First();
                    firstQuiz.QuizResults = QuizGroup.SelectMany(quiz => quiz.QuizResults).GroupBy(qr => qr.Id).Select(quizres =>
                    {
                        var firstResult = quizres.First();
                        return firstResult;
                    }).ToList();
                    return firstQuiz;
                }).SingleOrDefault();
            }
        }

        public async Task<IEnumerable<Quiz>> GetAllQuiz()
        {
            var sql = "SELECT * FROM Quizzes";
                
            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<Quiz>(sql);
            }
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizByTopicId(int id)
        {
            var sql = "SELECT q.Id, q.Name, q.Description, tp.Id, tp.Name FROM Quizzes q " +
                "INNER JOIN Topics tp ON tp.QuizId = q.Id  WHERE tp.Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Quiz, TopicDto, Quiz>(
                    sql,
                    (quiz, topic) =>
                    {
                        quiz.Topics.Add(topic);
                        return quiz;
                    },
                    new { id });

                return result.GroupBy(s => s.Id).Select(QuizGroup =>
                {
                    var firstQuiz = QuizGroup.First();
                    firstQuiz.Topics = QuizGroup.SelectMany(quiz => quiz.Topics).GroupBy(t => t.Id).Select(topic =>
                    {
                        var firstTopic = topic.First();
                        return firstTopic;
                    }).ToList();
                    return firstQuiz;
                });
            }
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizByTakerId(int id)
        {
            var sp = "[spQuiz_GetAllQuizByTakerId]";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Quiz, TakerDto, Quiz>(
                    sp,
                    (quiz, taker) =>
                    {
                        quiz.Takers.Add(taker);
                        return quiz;
                    },
                    new { id }, commandType: CommandType.StoredProcedure);

                return result.GroupBy(s => s.Id).Select(QuizGroup =>
                {
                    var firstQuiz = QuizGroup.First();
                    firstQuiz.Takers = QuizGroup.SelectMany(quiz => quiz.Takers).GroupBy(t => t.Id).Select(taker =>
                    {
                        var firstTaker = taker.First();
                        return firstTaker;
                    }).ToList();
                    return firstQuiz;
                });
            }
        }

        public async Task<bool> UpdateQuiz(Quiz quiz)
        {
            var sql = "UPDATE Quizzes SET Name = @Name, Description = @Description WHERE Id = @Id " +
                "UPDATE QuizResults SET QuizName = @Name WHERE QuizId = @Id;";

            using (var con = _context.CreateConnection())
            {
                var rowsAffected = await con.ExecuteAsync(sql, new { quiz.Name, quiz.Description, quiz.Id });
                return rowsAffected > 0 ? true : false;
            }
        }

        public async Task<bool> CheckQuizId(int id)
        {
            var sql = "SELECT * FROM Quizzes WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(sql, new { id });
            }
        }

        public async Task<bool> DeleteQuiz(int id)
        {
            var sp = "[spQuiz_DeleteQuiz]";

            using (var con = _context.CreateConnection())
            {
                int rowAffected = await con.ExecuteAsync(sp, new { id }, commandType: CommandType.StoredProcedure);
                return rowAffected > 0 ? true : false;
            }
        }

        private static Quiz MapQuizTopic(Quiz quiz, TopicDto topic)
        {
            quiz.Topics.Add(topic);
            return quiz;
        }
    }
}
