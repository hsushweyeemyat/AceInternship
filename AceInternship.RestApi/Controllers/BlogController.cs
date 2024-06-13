using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace AceInternship.RestApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogController : ControllerBase
	{
		private readonly SqlConnectionStringBuilder _connectionStringBuilder;

		public BlogController()
		{
			/*_connectionStringBuilder = new SqlConnectionStringBuilder();
			_connectionStringBuilder.DataSource = ".";
			_connectionStringBuilder.InitialCatalog = "AceInternship";
			_connectionStringBuilder.UserID = "sa";
			_connectionStringBuilder.Password = "sa@123";*/

			//or

			_connectionStringBuilder = new SqlConnectionStringBuilder()
			{
			DataSource = ".",
			InitialCatalog = "AceInternship",
			UserID = "sa",
			Password = "sasa@123"
		};
		}

		[HttpGet]
		public IActionResult GetBlogs()
		{
			using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
			var lst = db.Query<TblBLog>(Queries.Bloglist).ToList();
			return Ok(lst);
		}
		
		[HttpGet("{id}")]
		public IActionResult GetBlogs(int id)
		{
			using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
			var item = db.Query<TblBLog>(Queries.BlogById,new {BlogId = id}).FirstOrDefault();
			if (item is null)
			{
				return NotFound("No data found.");
			}
			return Ok(item);
		}
		[HttpPost]
		public IActionResult CreateBlog(TblBLog blog)
		{
			using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
			int result = db.Execute(Queries.BlogCreate, blog);
			string message = result > 0 ? "Update Successful." : "Update Failed.";
			return Ok(message);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateBlogs(int id,TblBLog blog)
		{
			blog.BlogId = id;
			using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
			int result = db.Execute(Queries.BLogUpdate, blog);
			string message = result > 0 ? "Saving Successful." : "Saving Failed.";
			return Ok(message);
			
		}

		[HttpPatch]
		public IActionResult PatchBlogs()
		{
			return Ok("Patch Blogs");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteBlog(int id)
		{

			using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
			int result = db.Execute(Queries.BLogDelete, new { BlogId = id });
			string message = result > 0 ? "Delete Successful." : "Delete Failed.";

			return Ok(message);
		}

		public class TblBLog
		{
			public int BlogId { get; set; }
			public string BlogTitle { get; set; }
			public string BlogAuthor { get; set; }
			public string BlogContent { get; set; }
		}

		public static class Queries
		{
			public static string Bloglist { get; } = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog]";

			public static string BlogById { get; } = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog] Where BlogId = @BlogId";

			public static string BlogCreate { get; } = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

			public static string BLogUpdate { get; } = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

			public static string BLogDelete { get; } = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";
		}
	}
	
}
