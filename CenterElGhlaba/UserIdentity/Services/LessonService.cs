using AutoMapper;
using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Center_ElGhlaba.Services
{
    public class LessonService : ILessonService
    {
        private IValidationDictionary _validationDictionary;
        private IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

        public LessonService(IValidationDictionary validationDictionary, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _validationDictionary = validationDictionary;
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LessonDetailsVM> GetLessonDetails(int id, string? userID)
        {
            Lesson lesson = await _UnitOfWork.Lessons.FindAsync(l => l.ID == id, new[] { "Teacher.AppUser", "Subject", "Level", "Comments.Student.AppUser", });
            var result = _mapper.Map<LessonDetailsVM>(lesson);

            return result;
        }
        //protected bool ValidateLesson(Lesson lessonToValidate)
        //{
        //    if (lessonToValidate == null) { return false; }

        //}
        //public bool CreateLesson(Lesson lessonToCreate)
        //{

        //}
    }
}
