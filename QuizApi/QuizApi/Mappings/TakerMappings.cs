using AutoMapper;
using QuizApi.Dtos.Taker;
using QuizApi.Models;

namespace QuizApi.Mappings
{
    public class TakerMappings : Profile
    {
        public TakerMappings()
        {
            CreateMap<TakerCreationDto, Taker>();
            CreateMap<Taker, TakerDto>();
            CreateMap<Taker, TakerQuizDto>()
                  .ForMember(dto => dto.QuizName, opt => opt.MapFrom(st => st.Quizzes!.Single().Name));
            CreateMap<Taker, TakerQuizzesDto>();
            CreateMap<Taker, TakerQuizResultDto>();
            CreateMap<Taker, TakerAnswersDto>();
        }
    }
}
