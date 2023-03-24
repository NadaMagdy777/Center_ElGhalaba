using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace Center_ElGhlaba.Hubs
{
    public class LessonHub:Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        public LessonHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddLesson(LessonVM newLesson)
        {

            Clients.All.SendAsync("NewLessonAdded", newLesson);
        }

        public override Task OnConnectedAsync()
        {

            return base.OnConnectedAsync();
        }
    }
}
