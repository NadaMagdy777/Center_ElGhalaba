using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Center_ElGhlaba.Hubs
{
    public class LessonHub:Hub
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LessonHub(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }
        public override async Task<Task> OnConnectedAsync()
        {
            if (Context.User.IsInRole("Student")) { 
                string userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Student student = await unitOfWork.Students.FindAsync(s => s.AppUserID == userId);
                List<Follows> follows = await unitOfWork.Follows.FindAllAsync(f => f.StudentID == student.ID);
                foreach(Follows f in follows)
                {
                    Groups.AddToGroupAsync(Context.ConnectionId, f.TeacherID.ToString());
                }
            }

            return base.OnConnectedAsync();
        }
    }
}
