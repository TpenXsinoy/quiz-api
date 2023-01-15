using AutoMapper;
using QuizApi.Dtos.Question;
using QuizApi.Dtos.QuizD;
using QuizApi.Dtos.Taker;
using QuizApi.Models;

namespace QuizApi.Mappings
{
    public class QuizMappings : Profile
    {
        public QuizMappings()
        {
            CreateMap<QuizCreationDto, Quiz>();
            CreateMap<Quiz, QuizDto>();
            CreateMap<Quiz, QuizTopicDto>().ForMember(q => q.TopicName, opt => opt.MapFrom(x => x.Topics[0].Name));
            CreateMap<Quiz, QuizTakerDto>().ForMember(q => q.TakerName, opt => opt.MapFrom(x => x.Takers[0].Name));
            CreateMap<Quiz, QuizTakersDto>();
            CreateMap<Quiz, QuizTopicsDto>();
            CreateMap<Quiz, QuizQuizResultDto>();
        }
        
    }
}
