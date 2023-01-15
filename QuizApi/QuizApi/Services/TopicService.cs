using AutoMapper;
using QuizApi.Dtos.Taker;
using QuizApi.Dtos.Topic;
using QuizApi.Models;
using QuizApi.Repositories;

namespace QuizApi.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public TopicService (ITopicRepository repository, IMapper mapper)
        {
            _topicRepository = repository;
            _mapper = mapper;
        }

        public async Task<TopicDto> CreateTopic(TopicCreationDto topicToCreate, int quizId)
        {
            var topicModel = _mapper.Map<Topic>(topicToCreate);
            topicModel.Id = await _topicRepository.CreateTopic(topicModel, quizId);

            var topicDto = _mapper.Map<TopicDto>(topicModel);
            return topicDto;
        }

        public async Task<IEnumerable<TopicDto>> GetAllTopics()
        {
            var topicModels = await _topicRepository.GetAllTopics();
            return _mapper.Map<IEnumerable<TopicDto>>(topicModels);
        }

        public async Task<IEnumerable<TopicQuizDto>> GetAllTopics(int quizId)
        {
            var topicModels = await _topicRepository.GetAllTopicsByQuizId(quizId);
            return _mapper.Map<IEnumerable<TopicQuizDto>>(topicModels);
        }

        public async Task<TopicDto?> GetTopicById(int id)
        {
            var topicModel = await _topicRepository.GetTopic(id);
            if (topicModel == null) return null;

            return _mapper.Map<TopicDto>(topicModel);
        }

        public async Task<Topic?> GetTopicWithQuestion(int id)
        {
            return await _topicRepository.GetTopicWithQuestion(id);
        }

        public async Task<TopicDto> UpdateTopic(int id, TopicCreationDto topicUpdate)
        {
            var topicModel = _mapper.Map<Topic>(topicUpdate);
            topicModel.Id = id;
            await _topicRepository.UpdateTopic(topicModel);
            var topicDto = _mapper.Map<TopicDto>(topicModel);

            return topicDto;
        }

        public async Task<bool> DeleteTopic(int id)
        {
            return await _topicRepository.DeleteTopic(id);
        }
    }
}
