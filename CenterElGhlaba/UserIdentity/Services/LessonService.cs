using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Center_ElGhlaba.Services
{
    public class LessonService : ILessonService
    {
        private IValidationDictionary _validationDictionary;
        private IUnitOfWork _UnitOfWork;

        public LessonService(IValidationDictionary validationDictionary, IUnitOfWork unitOfWork)
        {
            _validationDictionary = validationDictionary;
            _UnitOfWork = unitOfWork;
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
