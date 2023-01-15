using QuizApi.Dtos.QuizResult;
using QuizApi.Models;
using QuizApi.Repositories;
using AutoMapper;

namespace QuizApi.Services
{
    public class QuizResultService : IQuizResultService
    {
        private readonly IQuizResultRepository _quizResultRepository;
        private readonly IMapper _mapper;

        public QuizResultService(IQuizResultRepository repository, IMapper mapper)
        {
            _quizResultRepository = repository;
            _mapper = mapper;
        }
        public async Task<QuizResultDto> CreateQuizResult(QuizResultCreationDto quizresultToCreate)
        {
            //Convert Dto to Models
            var quizResultModel = _mapper.Map<QuizResult>(quizresultToCreate);
            quizResultModel.Id = await _quizResultRepository.CreateQuizResult(quizResultModel);

            var quizResultDto = _mapper.Map<QuizResultDto>(quizResultModel);
            return quizResultDto;
        }

        public async Task<IEnumerable<QuizResultDto>> GetAllQuizResult()
        {
            var quizResults = await _quizResultRepository.GetAllQuizResult();
            return _mapper.Map<IEnumerable<QuizResultDto>>(quizResults);
        }

        public async Task<QuizResultDto?> GetQuizResultById(int id)
        {
            var quizResultModel = await _quizResultRepository.GetQuizResultById(id);
            if (quizResultModel == null) return null;

            return _mapper.Map<QuizResultDto>(quizResultModel);
        }

        public async Task<QuizResultDto> UpdateQuizResult(int id, QuizResultUpdateDto quizResultToUpdate)
        {
            var quizResultModel = _mapper.Map<QuizResult>(quizResultToUpdate);
            quizResultModel.Id = id;
            await _quizResultRepository.UpdateQuizResult(quizResultModel);
            var quizResultDto = _mapper.Map<QuizResultDto>(quizResultModel);

            return quizResultDto;
        }
        public async Task<bool> DeleteQuizResult(int id)
        {
            return await _quizResultRepository.DeleteQuizResult(id);
        }
    }
}
