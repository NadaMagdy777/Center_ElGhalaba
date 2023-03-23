using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Center_ElGhlaba.Hubs
{
    public class CommentHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentHub(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddComment(int lessonId, int studentId, string comment, DateTime date)
        {
            LessonComment newComment = new LessonComment()
            {
                LessonID = lessonId,
                StudentID = studentId,
                Body = comment,
                Date = date,
                IsDeleted = false
            };

            _unitOfWork.comments.Insert(newComment);
            _unitOfWork.Complete();

            await Clients.All.SendAsync("CommentAdded", lessonId, studentId, comment, date);
        }
    }
}
