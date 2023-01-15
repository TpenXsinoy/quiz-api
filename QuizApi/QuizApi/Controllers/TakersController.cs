using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Dtos.Taker;
using QuizApi.Services;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TakersController : ControllerBase
    {
        private readonly ILogger<TakersController> _logger;
        private readonly ITakerService _takerService;
        private readonly IQuizService _quizService;

        public TakersController(
            ILogger<TakersController> logger, 
            ITakerService takerService, 
            IQuizService quizService)
        {
            _logger = logger;
            _takerService = takerService;
            _quizService = quizService;
        }

        /// <summary>
        /// Creates a taker
        /// </summary>
        /// <param name="taker">Taker Details</param>
        /// <returns>Returns the newly created taker</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Takers
        ///     {
        ///         "name" : "Jhonray Asohedo",
        ///         "address" : "Talisay City, Cebu",
        ///         "email" : "jhonray.asohedo@gmail.com"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created a taker</response>
        /// <response code="400">Taker details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTaker([FromBody] TakerCreationDto taker)
        {
            try
            {
                var newTaker = await _takerService.CreateTaker(taker);
                return CreatedAtRoute("GetTakerById", new { id = newTaker.Id }, newTaker);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all takers or Gets all takers assigned to a quiz
        /// </summary>
        /// <param name="quizId">Quiz Id</param>
        /// <returns>Returns all takers</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Takers
        ///     {
        ///         "id": 1,
        ///         "name" : "John Doe",
        ///         "address" : "N. Bacalso Ave, Cebu City",
        ///         "email" : "johndoe@gmail.com"
        ///     }
        ///     GET api/Takers?quizId=1
        ///     {
        ///         "id": 1,
        ///         "name": "John Doe",
        ///         "address": "N. Bacalso Ave, Cebu City",
        ///         "email": "johndoe@gmail.com",
        ///         "quizName": "Math Quiz"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved takers</response>
        /// <response code="204">There are no takers</response>
        /// <response code="404">Quiz with the given quizId is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllTakers")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TakerQuizDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTakers([FromQuery] int quizId)
        {
            try
            {
                var takers = await _takerService.GetAllTakers();
                var quiz = await _quizService.GetQuizById(quizId);
                var takersWithQuiz = await _takerService.GetAllTakers(quizId);

                if (quiz == null && quizId != 0)
                    return NotFound($"Quiz with Id = {quizId} is not found");

                if (quizId == 0 && takers.IsNullOrEmpty())
                    return NoContent();

                if (quizId == 0)
                    return Ok(takers);
                else
                    return Ok(takersWithQuiz);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets taker
        /// </summary>
        /// <param name="id">Taker Id</param>
        /// <returns>Returns taker</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Takers/2
        ///     {
        ///         "id": 2,
        ///         "name": "Jane Doe",
        ///         "address": "Somewhere bukid, Cebu City",
        ///         "email": "janedoe@gmail.com",
        ///         "quizzes": [
        ///             {
        ///                 "id": 1,
        ///                 "name": "Math Quiz",
        ///                 "description": "Calculus quiz"
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved taker</response>
        /// <response code="404">Taker with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetTakerById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerQuizzesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTaker(int id)
        {
            try
            {
                var taker = await _takerService.GetTakerById(id);
                var takerWithQuiz = await _takerService.GetTakerWithQuizById(id);

                if (taker == null && takerWithQuiz == null)
                    return NotFound($"Taker with id {id} does not exist");

                if(takerWithQuiz != null)
                    return Ok(takerWithQuiz);
                else
                    return Ok(taker);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets taker with quiz results
        /// </summary>
        /// <param name="id">Taker Id</param>
        /// <returns>Returns taker with quiz results</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Takers/2/quizResults 
        ///     {
        ///         "id": 2,
        ///         "name": "Jane Doe",
        ///         "address": "Somewhere bukid, Cebu City",
        ///         "email": "janedoe@gmail.com",
        ///         "quizResults": [
        ///             {
        ///                 "id": 2,
        ///                 "quizName": "Math Quiz",
        ///                 "score": 5,
        ///                 "evaluation": "Passed!"
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved taker with quiz results</response>
        /// <response code="204">Taker with the given id has no quiz results yet</response>
        /// <response code="404">Taker with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}/quizResults", Name = "GetTakerQuizResultById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerQuizResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTakerQuizResults(int id)
        {
            try
            {
                var taker = await _takerService.GetTakerById(id);
                var takerQuizResult = await _takerService.GetTakerWithQuizResultById(id);

                if (taker == null && takerQuizResult == null)
                    return NotFound($"Taker with id {id} does not exist");
                else if(takerQuizResult == null)
                    return NoContent();

                return Ok(takerQuizResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets taker with answers
        /// </summary>
        /// <param name="id">Taker Id</param>
        /// <returns>Returns taker with answers</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Takers/1/answers 
        ///     {
        ///         "id": 1,
        ///         "name": "John Doe",
        ///         "address": "N. Bacalso Ave, Cebu City",
        ///         "email": "johndoe@gmail.com",
        ///         "takerAnswers": [
        ///             {
        ///                 "id": 1,
        ///                 "question": "1+1=3",
        ///                 "answer": "True",
        ///                 "status": "Wrong"
        ///             },
        ///             {
        ///                 "id": 2,
        ///                 "question": "Earth is flat",
        ///                 "answer": "True",
        ///                 "status": "Correct"
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved taker with answers</response>
        /// <response code="204">Taker with the given id has no answers yet</response>
        /// <response code="404">Taker with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}/answers", Name = "GetTakerAnswersById")] 
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerAnswersDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTakerAnswers(int id)
        {
            try
            {
                var taker = await _takerService.GetTakerById(id);
                var takerAnswers = await _takerService.GetTakerAnswersById(id);

                if (taker == null && takerAnswers == null)
                    return NotFound($"Taker with id {id} does not exist");
                else if (takerAnswers == null)
                    return NoContent();

                return Ok(takerAnswers);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }


        /// <summary>
        /// Updates a taker
        /// </summary>
        /// <param name="id">Taker Id</param>
        /// <param name="takerToBeUpdated">Taker update details</param>
        /// <returns>Returns the newly updated taker</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Takers
        ///     {
        ///         "name" : "Jhonray Acohedo",
        ///         "address" : "Talisay City, Cebu",
        ///         "email" : "jhonray.acohedo@gmail.com"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully updated a taker</response>
        /// <response code="400">Taker details are invalid</response>
        /// <response code="404">Taker is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTaker(int id, [FromBody] TakerCreationDto takerToBeUpdated)
        {
            try
            {
                var taker = await _takerService.GetTakerById(id);

                if (taker == null)
                    return NotFound($"Taker with Id = {id} is not found");

                var updatedTaker = await _takerService.UpdateTaker(id, takerToBeUpdated);
                return Ok(updatedTaker);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes taker
        /// </summary>
        /// <param name="id">Taker Id</param>
        /// <returns>Successful deletion message</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/Takers/1
        ///             Taker with Id = 1 was Successfully Deleted
        /// 
        /// </remarks>
        /// <response code="200">Successfully deleted taker</response>
        /// <response code="404">Taker with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")] 
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTaker(int id)
        {
            try
            {
                var taker = await _takerService.GetTakerById(id);
               
                if (taker == null)
                    return NotFound($"Taker with Id = {id} is not found");

                await _takerService.DeleteTaker(id);
                return Ok($"Taker with Id = {id} was Successfully Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Lets taker take quizzes
        /// </summary>
        /// <param name="takerId">Taker Id</param>
        /// <param name="quizId">Quiz Id</param>
        /// <returns>Returns taker with newly taken quiz</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Takers/takeQuiz?takerId=3quizId=2
        ///     {
        ///         "id": 3,
        ///         "name": "Stephine Doe",
        ///         "address": "Cebu City",
        ///         "email": "stephine.doe@gmail.com",
        ///         "quizzes": [
        ///             {
        ///                 "id": 1,
        ///                 "name": "Math Quiz",
        ///                 "description": "Calculus quiz",
        ///             }
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully assigned taker to a quiz</response>
        /// <response code="400">Taker has already taken the Quiz</response>
        /// <response code="404">Taker with the given takerId or Quiz with the given quizId is not found or both are not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("takeQuiz", Name = "LetTakerTakeQuiz")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerQuizzesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LetTakerTakeQuiz([FromQuery] int takerId, [FromQuery] int quizId)
        {
            try
            {
                var taker = await _takerService.GetTakerById(takerId);
                var quiz = await _quizService.GetQuizById(quizId);
                var hasTakerTakenQuiz = await _takerService.HasTakerTakenQuiz(takerId, quizId);

                if(taker == null && quiz == null)
                    return NotFound($"Taker with Id = {takerId} and Quiz with Id = {quizId} are not found");
                else if (taker == null)
                    return NotFound($"Taker with Id = {takerId} is not found");
                else if (quiz == null)
                    return NotFound($"Quiz with Id = {quizId} is not found");

                if (hasTakerTakenQuiz)
                    return BadRequest($"Taker with Id = {takerId} has already taken Quiz with Id = {quizId}");

                await _takerService.LetTakerTakeQuiz(takerId, quizId);
                var takerWithQuiz = await _takerService.GetTakerWithQuizById(takerId);

                return Ok(takerWithQuiz);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
