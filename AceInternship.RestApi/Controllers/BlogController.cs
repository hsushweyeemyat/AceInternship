using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AceInternship.RestApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogController : ControllerBase
	{
		[HttpGet]
		public IActionResult GetBlogs()
		{
			return Ok("Get Blogs");
		}
		[HttpPost]
		public IActionResult PostBlogs()
		{
			return Ok("Post Blogs");
		}
		[HttpPut]
		public IActionResult PutBlogs()
		{
			return Ok("Put Blogs");
		}
		[HttpPatch]
		public IActionResult PatchBlogs()
		{
			return Ok("Patch Blogs");
		}
		[HttpDelete]
		public IActionResult DeleteBlog()
		{
			return Ok("Delete Blog");
		}
	}
}
