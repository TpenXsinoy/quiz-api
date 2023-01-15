using AutoMapper;
using QuizApi.Dtos.Taker;
using QuizApi.Models;
using QuizApi.Repositories;

namespace QuizApi.Services
{
    public class TakerService : ITakerService
    {
        private readonly ITakerRepository _takerRepository;
        private readonly IMapper _mapper;

        public TakerService(ITakerRepository repository, IMapper mapper)
        {
            _takerRepository = repository;
            _mapper = mapper;
        }

        public async Task<TakerDto> CreateTaker(TakerCreationDto takerToCreate)
        {
            var takerModel = _mapper.Map<Taker>(takerToCreate);
            takerModel.Id = await _takerRepository.CreateTaker(takerModel);

            var takerDto = _mapper.Map<TakerDto>(takerModel);
            return takerDto;
        }

        public async Task<IEnumerable<TakerDto>> GetAllTakers()
        {
            var takerModels = await _takerRepository.GetAll();
            return _mapper.Map<IEnumerable<TakerDto>>(takerModels);
        }

        public async Task<IEnumerable<TakerQuizDto>> GetAllTakers(int quizId)
        {
            var takerModels = await _takerRepository.GetAllByQuizId(quizId);
            return _mapper.Map<IEnumerable<TakerQuizDto>>(takerModels);
        }

        public async Task<TakerQuizzesDto?> GetTakerById(int id)
        {
            var takerModel = await _takerRepository.GetTaker(id);
            if (takerModel == null) return null;

            return _mapper.Map<TakerQuizzesDto>(takerModel);
        }

        public async Task<TakerQuizzesDto?> GetTakerWithQuizById(int id)
        {
            var takerModel = await _takerRepository.GetTakerWithQuiz(id);
            if (takerModel == null) return null;

            return _mapper.Map<TakerQuizzesDto>(takerModel);
        }

        public async Task<TakerQuizResultDto?> GetTakerWithQuizResultById(int id)
        {
            var takerModel = await _takerRepository.GetTakerWithQuizResult(id);
            if (takerModel == null) return null;

            return _mapper.Map<TakerQuizResultDto>(takerModel);
        }

        public async Task<TakerAnswersDto?> GetTakerAnswersById(int id)
        {
            var takerModel = await _takerRepository.GetTakerAnswers(id);
            if (takerModel == null) return null;

            return _mapper.Map<TakerAnswersDto>(takerModel);
        }

        public async Task<bool> DeleteTaker(int id)
        {
            return await _takerRepository.DeleteTaker(id);
        }

        public async Task<TakerDto> UpdateTaker(int id , TakerCreationDto takerToUpdate)
        {
            var takerModel = _mapper.Map<Taker>(takerToUpdate);
            takerModel.Id = id;
            await _takerRepository.UpdateTaker(takerModel);
            var takerDto = _mapper.Map<TakerDto>(takerModel);

            return takerDto;
        }

        public async Task<int> LetTakerTakeQuiz(int takerId, int quizId)
        {
            return await _takerRepository.LetTakerTakeQuiz(takerId, quizId);
        }

        public async Task<bool> HasTakerTakenQuiz(int takerId, int quizId)
        {
            return await _takerRepository.HasTakerTakenQuiz(takerId, quizId);
        }
    }
}
