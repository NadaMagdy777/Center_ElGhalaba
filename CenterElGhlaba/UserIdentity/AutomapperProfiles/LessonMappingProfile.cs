using AutoMapper;
using Center_ElGhalaba.Models;
using Center_ElGhlaba.Models;
using Center_ElGhlaba.ViewModels;

namespace Center_ElGhlaba.AutomapperProfiles
{
    public class LessonMappingProfile : Profile
    {
        public LessonMappingProfile()
        {
            CreateMap<Lesson, LessonDetailsVM>()
                .ForMember(dest => dest.LessonID, src => src.MapFrom(src => src.ID))
                .ReverseMap();

            CreateMap<Student, LessonDetailsVM>().ReverseMap();
            CreateMap<Teacher, LessonDetailsVM>().ReverseMap(); 
            CreateMap<Subject, LessonDetailsVM>().ReverseMap();
            CreateMap<Level, LessonDetailsVM>().ReverseMap();

            CreateMap<IEnumerable<LessonLikes>, LessonDetailsVM >().ReverseMap();
            CreateMap<IEnumerable<LessonViews>, LessonDetailsVM>().ReverseMap();
            CreateMap<IEnumerable<LessonComment>, LessonDetailsVM>().ReverseMap();
            CreateMap<IEnumerable<LessonResource>, LessonDetailsVM>().ReverseMap();
            CreateMap<IEnumerable<StudentOrder>, LessonDetailsVM>().ReverseMap();
        }
    }
}
