using CarProject.Atributes;
using CarProject.Core.Mangers.Interfaces;
using CarProject.ModelViews.Request;
using CarProject.ModelViews.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarProject.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BlogController : ApiBaseController
    {
        private readonly IBlogManager _blogManager;

        public BlogController(ILogger<UserController> logger , IBlogManager blogManager)
        {
            _blogManager = blogManager;
        }
        [HttpGet]
        public IActionResult GetBlogs(int page = 1,
                                      int pageSize = 5,
                                      StatusEnum statusEnum = StatusEnum.All,
                                      string sortColumn = "",
                                      string sortDirection = "ascending",
                                      string searchText = "")
        {
            var result = _blogManager.GetBlogs(page, pageSize, statusEnum, sortColumn, sortDirection, searchText);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetBlog(int id)
        {
            var result = _blogManager.GetBlog(id);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CarProjectAuthrize]
        public IActionResult ArchiveBlog(int id)
        { 
            _blogManager.ArchiveBlog(LoggedInUser , id);
            return Ok();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CarProjectAuthrize]
        public IActionResult PutBlog(BlogRequest blogRequest)
        {
            var result = _blogManager.PutBlog(LoggedInUser, blogRequest);
            return Ok(result);
        }


    }
}
