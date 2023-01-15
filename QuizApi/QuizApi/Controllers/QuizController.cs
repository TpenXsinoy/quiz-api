using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Dtos.QuizD;
using QuizApi.Services;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : Controller
    {
        private readonly IQuizService _quizService;
        private readonly ITopicService _topicService;
        private readonly ITakerService _takerService;
        private readonly ILogger<QuizzesController> _logger;

        public QuizzesController(
            IQuizService QuizService, 
            ILogger<QuizzesController> logger, 
            ITopicService topicService, 
            ITakerService takerService)
        {
            _takerService = takerService;
            _topicService = topicService;
            _quizService = QuizService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a quiz
        /// </summary>
        /// <param name="quiz">Quiz Details</param>
        /// <returns>Returns the newly created quiz</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Quizzes
        ///     {
        ///         "name" : "Math Quiz",
        ///         "description" : "Simple adding and subtracting quiz!"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created a quiz</response>
        /// <response code="400">Quiz details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Consumes("application/JSON")]
        [Produces("application/JSON")]
        [ProducesResponseType(typeof(QuizDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizCreationDto quiz)
        {
            var newQuiz = await _quizService.CreateQuiz(quiz);
            return CreatedAtRoute("GetQuizById", new { id = newQuiz.Id }, newQuiz);
        }

        /// <summary>
        /// Gets all quizzes or gets all Quizzes that a Topic is assigned to or gets all Quizzes that a Taker has answered
        /// </summary>
        /// <returns>Returns all quizzes or returns all quizzes that a topic is assigned to or
        /// returns all quizzes that a taker has answered</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Quizzes
        ///     {
        ///         "id": 1,
        ///         "name" : "Quiz1",
        ///         "description" : "Hard Quiz",
        ///     }
        ///     GET api/Quizzes?TopicId=2
        ///     {
        ///         "id": 2,
        ///         "name": "Quiz 2",
        ///         "description": "Hard quiz",     
        ///         "topicName": "Bio"
        ///     }
        ///     
        ///     GET api/Quizzes?TakerId=2
        ///     {
        ///         "id": 2,
        ///         "name": "Quiz 2",
        ///         "description": "Hard quiz",     
        ///         "takerName": "John Quiz"
        ///     }
        /// </remarks>
        /// <response code="200">Successfully retrieved quizzes</response>
        /// <response code="204">There are no quizzes available</response>
        /// <response code="400">Both Topic id and Taker Id are supplied</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllQuizzes")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(QuizDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(QuizTopicDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllQuizzes([FromQuery] int TopicId, [FromQuery] int TakerId)
        {
            try
            {
                if (TopicId != 0 && TakerId != 0)
                    return BadRequest($"User must choose either Topic Id or Taker Id to query but not both!");

                var quizzes = await _quizService.GetAllQuizzes();
                var topic = await _topicService.GetTopicById(TopicId);
                var taker = await _takerService.GetTakerById(TakerId);
                var quizWithTopic = await _quizService.GetAllQuizzesByTopicId(TopicId);
                var quizWithTaker = await _quizService.GetAllQuizzesByTakerId(TakerId);

                if (topic == null && TopicId != 0)
                    return NotFound($"Topic with Id = {TopicId} is not found");
                if (taker == null && TakerId != 0)
                    return NotFound($"Taker with Id = {TakerId} is not found");
                if (TopicId == 0 && quizzes.IsNullOrEmpty() || TakerId == 0 && quizzes.IsNullOrEmpty())
                    return NoContent();
                if (topic != null && quizWithTopic.IsNullOrEmpty())
                    return NotFound($"Given topic is not associated with any quizzes");
                if (taker != null && quizWithTaker.IsNullOrEmpty())
                    return NotFound($"Given taker is not associated with any quizzes");
                if (topic != null && quizWithTopic != null)
                    return Ok(quizWithTopic);
                if (taker != null && quizWithTaker != null)
                    return Ok(quizWithTaker);
                else
                    return Ok(quizzes);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets quiz by Id only or with topics
        /// </summary>
        /// <param name="id">Quiz Id</param>
        /// <returns>Returns quiz only or with topics</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Quizzes/1
        ///     {
        ///         "id": 1,
        ///         "name" : "Quiz1",
        ///         "description" : "Hard Quiz"
        ///     }
        ///     
        ///     GET /api/Quizzes/2
        ///     {
        ///         "id": 2,
        ///         "name": "Quiz 2",
        ///         "description": "Hard quiz",
        ///         "topics": [
        ///              {
        ///                "id": 1,
        ///                "name": "Math"
        ///              },
        ///              {
        ///                "id": 2,
        ///                "name": "Bio"
        ///              }
        ///           ]   
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved quiz</response>
        /// <response code="204">Quiz with the given id is not found</response>
        /// <response code="404">Quiz with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetQuizById")] 
        [Produces("application/json")]
        [ProducesResponseType(typeof(QuizTopicsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetQuiz(int id)
        {
            try
            {
                var quiz = await _quizService.GetQuizById(id);
                var quizWithTopics = await _quizService.GetQuizByIdWithTopics(id);

                if (quiz == null && quizWithTopics == null)
                    return NotFound($"Quiz with id {id} does not exist");

                if (quizWithTopics != null)
                    return Ok(quizWithTopics);
                else
                    return Ok(quiz);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
        
        /// <summary>
        /// Gets quiz with takers
        /// </summary>
        /// <param name="id">quiz Id</param>
        /// <returns>Returns quiz with takers</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Quizzes/2/takers
        ///     {
        ///         "id": 2,
        ///         "name": "Quiz 2",
        ///         "description": "Hard quiz",     
        ///         "takers": [
        ///              {
        ///                "id": 2,
        ///                "Name": "John Quiz",
        ///                "Address": Mountain
        ///                "Email": "john@gmail.com"
        ///              }
        ///          ]
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved quiz with takers</response>
        /// <response code="204">Quiz with the given id has no takers</response>
        /// <response code="404">Quiz with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}/takers", Name = "GetQuizByIdWithTakers")] 
        [Produces("application/json")]
        [ProducesResponseType(typeof(QuizTakersDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetQuizByIdWithTakers(int id)
        {
            try
            {
                var quiz = await _quizService.GetQuizByIdWithTakers(id);
                var checker = await _quizService.CheckQuizById(id);
                if (checker == false && quiz == null)
                    return NotFound($"Quiz with id {id} does not exist");
                else if (checker == true && quiz == null)
                    return NoContent();

                return Ok(quiz);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets quiz with quiz results
        /// </summary>
        /// <param name="id">quiz Id</param>
        /// <returns>Returns quiz with quiz results</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Quizzes/2/quizResults
        ///     {
        ///         "id": 2,
        ///         "name": "Quiz 2",
        ///         "description": "Hard quiz",
        ///         "quizResults": [
        ///              {
        ///                 "id": 2,
        ///                 "takerName": "John Doe",
        ///                 "score": 5,
        ///                 "evaluation": "Needs practice"
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved quiz with quiz results</response>
        /// <response code="204">Quiz with the given id has no quiz results</response>
        /// <response code="404">Quiz with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}/quizResults", Name = "GetQuizByIdWithQuizResults")] 
        [Produces("application/json")]
        [ProducesResponseType(typeof(QuizQuizResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetQuizByIdWithQuizResults(int id)
        {
            try
            {
                var quiz = await _quizService.GetQuizByIdWithQuizResults(id);
                var checker = await _quizService.CheckQuizById(id);
                if (checker == false && quiz == null)
                    return NotFound($"Quiz with id {id} does not exist");
                else if (checker == true && quiz == null)
                    return NoContent();

                return Ok(quiz);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates a quiz
        /// </summary>
        /// <param name="id">Quiz Id</param>
        /// <param name="quiz">Quiz to update details</param>
        /// <returns>Returns the newly updated quiz</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Quizzes
        ///     {
        ///         "name" : "Quiz1",
        ///         "description" : "Hard Quiz"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully updated a quiz</response>
        /// <response code="204">Quiz is not found</response>
        /// <response code="400">Quiz details are invalid</response>
        /// <response code="404">Quiz is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(QuizDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateQuiz(int id, [FromBody] QuizCreationDto quiz)
        {
            try
            {
                bool doesQuizExist = await _quizService.CheckQuizById(id);
                if (!doesQuizExist)
                {
                    return NotFound($"Quiz with Id = {id} not found");
                }

                var updatedQuiz = await _quizService.UpdateQuiz(id, quiz);
                return Ok(updatedQuiz);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500,  "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes a quiz
        /// </summary>
        /// <param name="id">Quiz Id</param>
        /// <returns>Successful deletion message</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/Quizzes/1
        ///            Delete Successful!
        /// 
        /// </remarks>
        /// <response code="200">Successfully deleted quiz</response>
        /// <response code="204">Quiz is not found</response>
        /// <response code="404">Quiz with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            try
            {
                bool doesQuizExist = await _quizService.CheckQuizById(id);
                if (!doesQuizExist)
                {
                    return NotFound($"Quiz with Id = {id} not found");
                }
                await _quizService.DeleteQuiz(id);
                return Ok("Delete Successful!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
