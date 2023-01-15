using System.Data;
using Dapper;
using QuizApi.Context;
using QuizApi.Dtos.QuizD;
using QuizApi.Dtos.QuizResult;
using QuizApi.Models;

namespace QuizApi.Repositories
{
    public class TakerRepository : ITakerRepository
    {
        /// <summary>
        /// Stores db server connection string initialized in the constructor to map objects
        /// </summary>
        private readonly DapperContext _context;

        /// <summary>
        /// Constructor where _context is initialized with db server connection string
        /// </summary>
        public TakerRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateTaker(Taker taker)
        {
            var sql = "INSERT INTO Takers (Name, Address, Email) VALUES (@Name, @Address, @Email); " +
               "SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, taker);
            }
        }

        public async Task<IEnumerable<Taker>> GetAll()
        {
            var sql = "[spTaker_GetAllTakers]";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Taker, QuizDto, Taker>(
                    sql, 
                    MapTakerQuiz, 
                    commandType: CommandType.StoredProcedure);

                return result.GroupBy(s => s.Id).Select(TakerGroup =>
                {
                    var firstTaker = TakerGroup.First();
                    firstTaker.Quizzes = TakerGroup.SelectMany(taker => taker.Quizzes).ToList();
                    return firstTaker;
                });
            }
        }

        public async Task<IEnumerable<Taker>> GetAllByQuizId(int quizId)
        {
            var sql = "[spTaker_GetAllByQuizId]";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Taker, QuizDto, Taker>(
                    sql, 
                    MapTakerQuiz, 
                    new { quizId }, 
                    commandType: CommandType.StoredProcedure);

                return result.GroupBy(s => s.Id).Select(TakerGroup =>
                {
                    var firstTaker = TakerGroup.First();
                    firstTaker.Quizzes = TakerGroup.SelectMany(taker => taker.Quizzes).ToList();
                    return firstTaker;
                });
            }
        }

        public async Task<Taker?> GetTaker(int id)
        {
            var sql = "SELECT t.Id, t.Name, t.Address, t.Email FROM Takers t WHERE  t.Id = @Id;";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Taker>(sql, new { id });
            }
        }

        public async Task<Taker?> GetTakerWithQuiz(int id)
        {
            var sql = "[spTaker_GetTakerWithQuizById]";

            using (var connection = _context.CreateConnection())
            {

                var result = await connection.QueryAsync<Taker, QuizDto, Taker>(
                    sql, 
                    MapTakerQuiz, 
                    new { id }, 
                    commandType: CommandType.StoredProcedure);

                return result.GroupBy(s => s.Id).Select(TakerGroup =>
                {
                    var firstTaker = TakerGroup.First();
                    firstTaker.Quizzes = TakerGroup.SelectMany(taker => taker.Quizzes).ToList();
                    return firstTaker;
                }).SingleOrDefault();
            }
        }

        public async Task<Taker?> GetTakerWithQuizResult(int id)
        {
            var sql = "[spTaker_GetTakerWithQuizResultById]";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Taker, QuizDto, QuizResultsForTakerDto, Taker>(
                    sql, 
                    ((taker, quiz, quizResult) => 
                    {
                        taker.Quizzes.Add(quiz);
                        taker.QuizResults.Add(quizResult);
                        return taker;
                    }), 
                    new { id },
                    commandType: CommandType.StoredProcedure);

                return result.GroupBy(s => s.Id).Select(TakerGroup =>
                {
                    var firstTaker = TakerGroup.First();
                    firstTaker.Quizzes = TakerGroup.SelectMany(taker => taker.Quizzes).ToList();
                    firstTaker.QuizResults = TakerGroup.SelectMany(taker => taker.QuizResults).ToList();
                    return firstTaker;
                }).SingleOrDefault();
            }
        }

        public async Task<Taker?> GetTakerAnswers(int id)
        {
            var sql = "[spTaker_GetTakerAnswerById]";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Taker, Problem, TakerAnswer, Taker>(
                    sql,
                    ((taker, question, takerAnswer) =>
                    {
                        takerAnswer.Question = question.Question;
                        taker.TakerAnswers.Add(takerAnswer);
                        return taker;
                    }),
                    new { id },
                    commandType: CommandType.StoredProcedure);

                return result.GroupBy(s => s.Id).Select(TakerGroup =>
                {
                    var firstTaker = TakerGroup.First();
                    firstTaker.TakerAnswers = TakerGroup.SelectMany(taker => taker.TakerAnswers).ToList();
                    return firstTaker;
                }).SingleOrDefault();
            }
        }

        public async Task<bool> UpdateTaker(Taker taker)
        {
            var sql = "UPDATE Takers SET Name = @Name, Address = @Address, Email = @Email WHERE Id = @Id" +
                " UPDATE QuizResults SET TakerName = @Name WHERE TakerId = @Id";

            using (var connection = _context.CreateConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(sql, new { taker.Name, taker.Address, taker.Email, taker.Id });
                return rowsAffected != 0;
            }
        }

        public async Task<bool> DeleteTaker(int id)
        {
            var sql = "[spTaker_DeleteTakerById]";

            using (var connection = _context.CreateConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(
                    sql, 
                    new { id }, 
                    commandType: CommandType.StoredProcedure);

                return rowsAffected != 0;
            }
        }

        public async Task<int> LetTakerTakeQuiz(int takerId, int quizId)
        {
            var sql = "INSERT INTO TakerQuiz (TakerId, QuizId) VALUES (@TakerId, @QuizId); " +
             "SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, new { TakerId = takerId, QuizId = quizId });
            }
        }

        public async Task<bool> HasTakerTakenQuiz(int takerId, int quizId)
        {
            var sql = "SELECT * FROM TakerQuiz tq WHERE tq.TakerId = @TakerId AND tq.QuizId = @QuizId";
           
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(sql, new { TakerId = takerId, QuizId = quizId });
            }
        }

        private static Taker MapTakerQuiz(Taker taker, QuizDto quiz)
        {
            taker.Quizzes.Add(quiz);
            return taker;
        }
    }
}
