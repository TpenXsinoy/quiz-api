 using QuizApi.Context;
using QuizApi.Models;
using Dapper;
using System.Data;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace QuizApi.Repositories
{
    public class QuizResultRepository : IQuizResultRepository
    {
        /// <summary>
        /// Stores db server connection string initialized in the constructor to map objects
        /// </summary>
        private readonly DapperContext _context;

        /// <summary>
        /// Constructor where _context is initialized with db server connection string
        /// </summary>
        public QuizResultRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateQuizResult(QuizResult quizResult)
        {
            var sql = "INSERT INTO QuizResults (QuizId, QuizName, TakerId, TakerName, Score, Evaluation) " +
                      "VALUES (@QuizId, @QuizName, @TakerId, @TakerName, @Score, @Evaluation); " +
                      "SELECT SCOPE_IDENTITY();";

            var quizId = GetQuizId(quizResult.QuizName, quizResult.TakerName);
            var takerId = GetTakerId(quizResult.QuizName, quizResult.TakerName);
            var score = GetQuizScores(quizResult.QuizName, quizResult.TakerName);
            var eval = GetEvaluation(quizResult);

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql, 
                    new { QuizId = quizId, 
                        quizResult.QuizName, 
                        TakerId = takerId, 
                        quizResult.TakerName, 
                        Score = score, 
                        Evaluation = eval });
            }
        }

        public async Task<bool> DeleteQuizResult(int id)
        {
            var sql = "DELETE FROM QuizResults WHERE Id = @id";
            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteAsync(sql, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<QuizResult>> GetAllQuizResult()
        {
            var sql = "SELECT * FROM QuizResults";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<QuizResult>(sql);
            }
        }

        public async Task<QuizResult> GetQuizResultById(int id)
        {
            var sql = "SELECT * FROM QuizResults WHERE Id = @id";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<QuizResult>(sql, new { id });
            }
        }

        public async Task<bool> UpdateQuizResult(QuizResult quizResult)
        {
            var sql = "UPDATE QuizResults SET Score = @Score, Evaluation = @Evaluation WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(sql, new { quizResult.Id, quizResult.Score, quizResult.Evaluation }) > 0;
            }
        }

        public int GetQuizScores(string? quizName, string? takerName)
        {
            var spName = "[spQuizResult_GetQuizScores]";

            using (var con = _context.CreateConnection())
            {
                return con.ExecuteScalar<int>(
                    spName,
                    new { QuizName = quizName, TakerName = takerName },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int GetQuizId(string? quizName, string? takerName)
        {
            var sql_quizid = "[spQuizResult_GetQuizId]";

            using (var con = _context.CreateConnection())
            {
                return con.ExecuteScalar<int>(
                    sql_quizid,
                    new { QuizName = quizName, TakerName = takerName },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int GetTakerId(string? quizName, string? takerName)
        {
            var sql_takerid = "[spQuizResult_GetTakerId]";

            using (var con = _context.CreateConnection())
            {
                return con.ExecuteScalar<int>(
                    sql_takerid,
                    new { QuizName = quizName, TakerName = takerName },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int GetTotalScore(string? quizName, string? takerName)
        {
            var sql_totalScore = "[spQuizResult_GetTotalScore]";

            using (var con = _context.CreateConnection())
            {
                return con.ExecuteScalar<int>(
                    sql_totalScore,
                    new { QuizName = quizName, TakerName = takerName },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public string GetEvaluation(QuizResult quizResult)
        {
            var totalScore = GetTotalScore(quizResult.QuizName, quizResult.TakerName);
            var score = GetQuizScores(quizResult.QuizName, quizResult.TakerName);
            var passingScore = totalScore * 0.5;

            if (score == totalScore)
            {
                return "Perfect!";
            }
            else if (score >= passingScore)
            {
                return "Passed!";
            }
            return "Failed";
        }

        public async Task<int> CheckTakerHasAnswers(string? quizName, string? takerName)
        {
            var sql_totalScore = "[spQuizResult_GetTotalScore]";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql_totalScore,
                    new { QuizName = quizName, TakerName = takerName },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
