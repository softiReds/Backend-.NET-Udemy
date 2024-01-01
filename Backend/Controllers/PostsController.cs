using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        IPostsService _titlesServices;

        public PostsController(IPostsService titlesServices)
        {
            _titlesServices = titlesServices;
        }

        [HttpGet]
        public async Task<IEnumerable<PostDTO>> Get() => await _titlesServices.Get();
    }
}
