using Microsoft.AspNetCore.Mvc;
using QuizApi.Dtos.QuizResult;
using QuizApi.Repositories;
using QuizApi.Services;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizResultsController : ControllerBase
    {
        private readonly IQuizResultService _quizResultService;
        private readonly ILogger<QuizResultsController> _logger;
        private readonly IQuizResultRepository _quizResultRepository;

        public QuizResultsController(
            IQuizResultService quizResultService, 
            IQuizResultRepository quizResultRepository, 
            ILogger<QuizResultsController> logger)
        {
            _logger = logger;
            _quizResultService = quizResultService;
            _quizResultRepository = quizResultRepository;
        }

        /// <summary>
        /// Creates a quiz result 
        /// </summary>
        /// <param name="quizResult"> Quiz result details</param>
        /// <returns>Returns newly created quiz result</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/QuizResults
        ///     {
        ///         "quizName" : "Math Quiz",
        ///         "takerName" : "Jane Doe"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">Successfully created a quiz result</response>
        /// <response code="400">Quiz result details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(QuizResultCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateQuizResult([FromBody] QuizResultCreationDto quizResult)
        {
            try
            {
                var checkTakerAnswers = await _quizResultRepository.CheckTakerHasAnswers(quizResult.QuizName, quizResult.TakerName);
                if (checkTakerAnswers == 0)
                    return BadRequest("Taker has not answered any questions.");

                var newQuizResult = await _quizResultService.CreateQuizResult(quizResult);
                // If successfully created, STATUS CODE IS 201
                CreatedAtRoute("GetQuizResultById", new { id = newQuizResult.Id }, newQuizResult);

                var getNewQuizResult = await _quizResultService.GetQuizResultById(newQuizResult.Id);
                return Ok(getNewQuizResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
        /// <summary>
        /// Gets all quiz results
        /// </summary>
        /// <returns>Returns all quiz results</returns>
        ///<remarks>
        /// Sample request:
        /// 
        ///     GET api/QuizResults
        ///     {
        ///         "id": 1,
        ///         "quizName": "Math Quiz",
        ///         "takerName": "John Doe",
        ///         "score": 10,
        ///         "evaluation": "Perfect!"
        ///     }
        ///     {
        ///         "id": 2,
        ///         "quizName": "Science Quiz",
        ///         "takerName": "Jane Doe",
        ///         "score": 8,
        ///         "evaluation": "Passed!"
        ///     }
        ///     {
        ///         "id": 3,
        ///         "quizName": "Calculus Quiz",
        ///         "takerName": "Josh Doe",
        ///         "score": 3,
        ///         "evaluation": "Failed"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved quiz results</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllQuizResults")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(QuizResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllQuizResult()
        {
            var quizResult = await _quizResultService.GetAllQuizResult();
            return Ok(quizResult);
        }

        /// <summary>
        /// Gets quiz result
        /// </summary>
        /// <param name="id">Quiz result Id</param>
        /// <returns>Returns quiz result with the given id</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/QuizResults/2
        ///     {
        ///         "id": 2,
        ///         "quizName": "Science Quiz",
        ///         "takerName": "Jane Doe",
        ///         "score": 8,
        ///         "evaluation": "Passed!"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved quiz result</response>
        /// <response code="404">Quiz result with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetQuizResultById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(QuizResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetQuizResultById(int id)
        {
            try
            {
                var quizResult = await _quizResultService.GetQuizResultById(id);

                if (quizResult == null)
                    return NotFound($"Quiz result with id {id} does not exist");

                return Ok(quizResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates a quiz result
        /// </summary>
        /// <param name="id">Quiz result id</param>
        /// <param name="quizResultsToUpdate">Quiz result updated details</param>
        /// <returns>Returns the newly updated quiz result</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/QuizResults/3
        ///     {
        ///         "score": 6,
        ///         "evaluation": "Passed!"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Successfully updated a quiz result</response>
        /// <response code="400">Quiz result details are invalid</response>
        /// <response code="404">Quiz result is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateQuizResult")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(QuizResultUpdateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateQuizResult(int id, [FromBody] QuizResultUpdateDto quizResultsToUpdate)
        {
            try
            {           
                var quizResult = await _quizResultService.GetQuizResultById(id);
                if (quizResult == null)
                    return NotFound($"QuizResult with Id = {id} is not found");
                
                var updatedQuizResult = await _quizResultService.UpdateQuizResult(id, quizResultsToUpdate);
                var getUpdatedQuizResult = await _quizResultService.GetQuizResultById(id);
                return Ok(getUpdatedQuizResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }



        /// <summary>
        /// Deletes a quiz result
        /// </summary>
        /// <param name="id">Quiz result id</param>
        /// <returns>Successful deletion message</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/QuizResults/1
        ///         Quiz result with Id = 1 was Successfully Deleted
        /// 
        /// </remarks>
        /// <response code="200">Successfully deleted quiz result</response>
        /// <response code="404">Quiz result with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteQuizResult(int id)
        {
            try
            {
                var quizResult = await _quizResultService.GetQuizResultById(id);

                if (quizResult == null)
                    return NotFound($"Quiz result with Id = {id} is not found");

                await _quizResultService.DeleteQuizResult(id);
                return Ok($"Quiz result with Id = {id} was Successfully Deleted");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }

        }
    }
}
