using AceInternship.RestApi.Models;
using AceInternship.RestApi.Services;
using AceInternship.Shared;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using static AceInternship.RestApi.Controllers.BlogController;

namespace AceInternship.RestApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogDapperController1 : ControllerBase
	{
		private readonly DapperService _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
		[HttpGet]
		public IActionResult GetBlog()
		{
			string query = "select * from Tbl_Blog";
			var lst = _dapperService.Query<BlogDto>(query);
			return Ok(lst);
		}

		[HttpGet("{id}")]
		public IActionResult EditBlog(int id)
		{
			var item = FindById(id);
			if (item is null)
			{
				return NotFound("No data found!!");
			}
			return Ok(item);
		}
		
		[HttpPost]
		public IActionResult CreateBlog(BlogDto blog)
		{
			string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

			int result = _dapperService.Execute(query, blog);

			string message = result > 0 ? "Saving Successful." : "Saving Failed.";
			return Ok(message);
		}		
		[HttpPut("{id}")]
		public IActionResult UpdateBlog(int id, BlogDto blog)
		{
			var item = FindById(id);
			if (item is null)
			{
				return NotFound("No Data Found!!");
			}
			blog.BlogId = id;
			string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

			int result = _dapperService.Execute(query, blog);

			string message = result > 0 ? "Update Successful." : "Update Failed.";
			return Ok(message);
		}		
		[HttpPatch("{id}")]
		public IActionResult PatchBlog(int id, BlogDto blog)
		{
			var item = FindById(id);
			if (item is null)
			{
				return NotFound("No data Found!");
			}

			string conditions = string.Empty;

			if (!string.IsNullOrEmpty(blog.BlogTitle))
			{
				conditions += "[BlogTitle] = @BlogTitle, ";
			}
			if (!string.IsNullOrEmpty(blog.BlogAuthor))
			{
				conditions += "[BlogAuthor] = @BlogAuthor, ";
			}
			if (!string.IsNullOrEmpty(blog.BlogContent))
			{
				conditions += "[BlogContent] = @BlogContent, ";
			}
			if (conditions.Length == 0)
			{
				return NotFound("No data to update.");
			}
			conditions = conditions.Substring(0, conditions.Length - 2);

			blog.BlogId = id;

			string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId = @BlogId";

			int result = _dapperService.Execute(query, blog);

			string message = result > 0 ? "Updating Successful." : "Updating Failed.";
			return Ok(message);
		}		
		[HttpDelete("{id}")]
		public IActionResult DeleteBlog(int id, BlogDto blog)
		{
			var item = FindById(id);
			if (item is null)
			{
				return NotFound("No Data Found!!");
			}
			blog.BlogId = id;
			string query = @"DELETE FROM Tbl_Blog where BlogId = @BlogId";

			using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
			int result = db.Execute(query, new BlogDto { BlogId = id});

			string message = result > 0 ? "Delete Successful." : "Delete Failed.";
			return Ok(message);
		}
		private BlogDto FindById(int id)
		{
			string query = "Select * From Tbl_Blog where BlogID = @BlogId";
			var item = _dapperService.QueryFirstOrDefault<BlogDto>(query, new BlogDto { BlogId = id });
			return item;
		}
	}
}
