using AutoMapper;
using QuizApi.Dtos.QuizResult;
using QuizApi.Models;

namespace QuizApi.Mappings
{
    public class QuizResultMappings : Profile
    {
        public QuizResultMappings()
        {
            CreateMap<QuizResult, QuizResultDto>();
            CreateMap<QuizResultUpdateDto, QuizResult>();
            CreateMap<QuizResultCreationDto, QuizResult>();
        }
    }
}
