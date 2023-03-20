using Center_ElGhalaba.Models;
using Center_ElGhlaba.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Data;

namespace Center_ElGhlaba.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class RemoteAdminController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public RemoteAdminController()
        {
            this.context = context;
        }


    }
}
